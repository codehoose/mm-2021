﻿using System.Collections.Generic;
using ManicMiner.Converter.Lib.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SK2D.Graphics;

namespace MonoManicMiner.Spectrum
{
    public class BaddieRenderer : SpriteSheet
    {
        private readonly Dictionary<MMMob, int> _horizonalMobs = new Dictionary<MMMob, int>();

        private MMMapFile _mapFile;
        private int _room;
        private float _time;

        public float AnimSpeed { get; set; } = 0.1f;

        public BaddieRenderer(Texture2D texture)
            : base(texture, 16)
        {
        }

        public void SetMapFile(MMMapFile mapFile, int room)
        {
            _mapFile = mapFile;
            _room = room;

            _horizonalMobs.Clear();

            var rm = _mapFile.rooms[_room];
            foreach (var horiz in rm.horizEnemies)
            {
                var x = horiz.pos.x;
                var blockId = horiz.graphic + (x & horiz.ani) / 2;
                _horizonalMobs.Add(horiz, blockId);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, float scale)
        {
            DrawHorizontals(spriteBatch, scale);
        }

        public override void Update(float deltaTime)
        {
            UpdateHorizontals(deltaTime);
        }

        private void UpdateHorizontals(float deltaTime)
        {
            _time += deltaTime;
            if (_time < AnimSpeed)
            {
                return;
            }

            _time -= AnimSpeed;

            var room = _mapFile.rooms[_room];

            foreach (var robot in room.horizEnemies)
            {
                var x = robot.pos.x;
                var y = robot.pos.y;

                if (robot.dir == 1)
                {
                    x = x - robot.speed;
                    var blockId = robot.graphic + (x & robot.ani) / 2;
                    if (x <= robot.minPos)
                    {
                        robot.dir = 0;
                        _horizonalMobs[robot] = blockId;
                    }
                    else
                    {
                        _horizonalMobs[robot] = blockId + robot.fli;
                    }
                }
                else
                {
                    x = x + robot.speed;
                    var blockId = robot.graphic + (x & robot.ani) / 2;
                    if (x > robot.maxPos)
                    {
                        robot.dir = 1;
                        _horizonalMobs[robot] = blockId + robot.fli;
                    }
                    else
                    {
                        _horizonalMobs[robot] = blockId;
                    }
                }

                robot.pos = new MMPoint { x = x, y = y };
            }
        }

        private void DrawHorizontals(SpriteBatch spriteBatch, float scale)
        {
            var room = _mapFile.rooms[_room];

            foreach(var robot in room.horizEnemies)
            {
                var blockId = _horizonalMobs[robot];
                var x = robot.pos.x & 248; // Clamp to the 8x8 blocks
                var y = robot.pos.y;
                SetFrame(blockId);
                var dest = new Rectangle((int)(x * scale), (int)(y * scale), (int)(CellSize * scale), (int)(CellSize * scale));
                Draw(spriteBatch, Texture, dest, Source);
            }
        }
    }
}
