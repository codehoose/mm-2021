﻿using System;
using System.Collections.Generic;
using ManicMiner.Converter.Lib.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SK2D.Graphics;

namespace MonoManicMiner.Spectrum
{
    public class MinerWillyRenderer : SpriteSheet
    {
        private int dir;
        private MMRoom _room;
        private int _roomId;
        private Action<GameStateType> _changeGameState;
        private int _willyFall;
        private int _state; // cWillym
        private int _input;
        private int _j;
        private int _js;
        private int _x;
        private int _y;

        private float _time;
        private int _jp;
        private int _cheat;
        private Texture2D _background;

        public bool GodMode { get; set; }

        public event EventHandler<int> IncrementScore;

        public event EventHandler<int> Jumping;

        public event EventHandler OnDeath;

        public MinerWillyRenderer(Texture2D texture, Texture2D background)
            : base(texture, 16)
        {
            _background = background;
        }

        public void KillWilly()
        {
            _state = 6;
        }

        private void Reset()
        {
            _state = 0;
            _js = 0;
            _j = 0;
            _jp = 0;
            _x = _room.willyStart.pos.x;
            _y = _room.willyStart.pos.y;
            Position = new Vector2(_x, _y);
            dir = _room.willyStart.dir;

            var blockId = dir == 1 ? (8 + ((_x & 15) >> 1)) : (_x & 15) >> 1;
            SetFrame(blockId);
        }

        public override void Draw(SpriteBatch spriteBatch, int scale)
        {
            base.Draw(spriteBatch, scale);
            if (_roomId == 19)
            {
                var dest = new Rectangle(0, 0, _background.Width * scale, _background.Height * scale);
                Image.Draw(spriteBatch, _background, dest, _background.Bounds, DrawColor);
            }
        }

        public void SetRoom(MMRoom room, int roomId, Action<GameStateType> changeGameState)
        {
            _room = room;
            _roomId = roomId;
            _changeGameState = changeGameState;

            var start = _room.willyStart;

            Position = new Vector2(start.pos.x, start.pos.y);
            dir = start.dir;
            _x = start.pos.x;
            _y = start.pos.y;

            // Reset everything here!
            Reset();
        }

        private int GetWillyInput()
        {
            var left = Keyboard.GetState().IsKeyDown(Keys.Left) ? 1 : 0;
            var right = Keyboard.GetState().IsKeyDown(Keys.Right) ? 2 : 0;
            var jump = Keyboard.GetState().IsKeyDown(Keys.Space) ? 4 : 0;

            return left | right | jump;
        }

        private void CheckRoboHit()
        {
            if (GodMode)
            {
                return;
            }

            CheckHorizontalRoboHit();
        }

        private void CheckHorizontalRoboHit()
        {
            foreach (var robot in _room.horizEnemies)
            {
                if (robot.dir == 0)
                {
                    if (dir != 0)
                    {
                        var robRect = new Rectangle(robot.pos.x & 248, robot.pos.y, 16, 16);
                        var willyRect = new Rectangle(_x & 248, _y, 16, 16);
                        if (robRect.Intersects(willyRect))
                        {
                            _state = 6;
                        }
                    }
                    else
                    {
                        var robRect = new Rectangle(robot.pos.x & 248, robot.pos.y, 16, 16);
                        var willyRect = new Rectangle(_x & 248, _y, 16, 16);
                        if (robRect.Intersects(willyRect))
                        {
                            _state = 6;
                        }
                    }
                }
                else
                {
                    if (dir != 0)
                    {
                        var robRect = new Rectangle(robot.pos.x & 248, robot.pos.y, 16, 16);
                        var willyRect = new Rectangle(_x & 248, _y, 16, 16);
                        if (robRect.Intersects(willyRect))
                        {
                            _state = 6;
                        }
                    }
                    else
                    {
                        var robRect = new Rectangle(robot.pos.x & 248, robot.pos.y, 16, 16);
                        var willyRect = new Rectangle(_x & 248, _y, 16, 16);
                        if (robRect.Intersects(willyRect))
                        {
                            _state = 6;
                        }
                    }
                }
            }
        }


        private int CheckWillyKillBlock()
        {
            if (GodMode)
            {
                return 0;
            }

            var block1 = GetBlock(_x, _y);
            var block2 = GetBlock(_x + 8, _y);
            var block3 = GetBlock(_x, _y + 8);
            var block4 = GetBlock(_x + 8, _y + 8);
            var block5 = GetBlock(_x, _y + 16);
            var block6 = GetBlock(_x + 8, _y + 16);

            var hit = 0;

            if (block1 == 5 || block2 == 5 || block3 == 5 || block4 == 5 || block5 == 5 || block6 == 5)
            {
                hit = 1;
            }

            if (block1 == 6 || block2 == 6 || block3 == 6 || block4 == 6 || block5 == 6 || block6 == 6)
            {
                hit = 1;
            }

            return hit;
        }

        private void CheckWillyConv()
        {
            if (WillyOnConv())
            {
                if (dir != _room.travelator.dir || (_input & 3) == 0)
                {
                    _input = _room.travelator.dir == 0 ? (_input & 253) | 1 : (_input & 254) | 2;
                }
            }
        }

        private bool WillyOnConv()
        {
            var block1 = GetBlock(_x, _y + 16);
            var block2 = GetBlock(_x + 8, _y + 16);
            
            return block1 == 7 || block2 == 7;
        }

        private void CheckWillyFall()
        {
            var block1 = GetBlock(_x, _y + 16);
            var block2 = GetBlock(_x + 8, _y + 16);

            if (block1 == 0 && block2 == 0)
            {
                _state = 4;
                _js = 0;
            }
        }

        private void CheckCrumb()
        {
            var block1 = GetCrumb(_x / 8, (_y / 8) + 2);
            var block2 = GetCrumb(_x / 8 + 1, _y / 8 + 2);

            if (block1 != 0)
            {
                block1--;
                SetCrumb(_x / 8, (_y / 8) + 2, block1);
                if (block1 == 0)
                {
                    ClearBlock(_x / 8, (_y / 8) + 2);
                }
            }

            if (block2 != 0)
            {
                block2--;
                SetCrumb((_x/ 8) + 1, (_y / 8) + 2, block2);
                if (block2 == 0)
                {
                    ClearBlock((_x / 8) + 1, (_y / 8) + 2);
                }
            }
        }

        private int GetBlock(int x, int y)
        {
            var roomBlocks = _room.blocks;
            var offset = ((y / 8) * 32) + (x / 8);
            return roomBlocks[offset];
        }

        private int GetCrumb(int x, int y)
        {
            var offset = (y * 32) + x;
            return _room.crumbs[offset];
        }

        private void SetCrumb(int x, int y, int crumb)
        {
            var offset = (y * 32) + x;
            _room.crumbs[offset] = crumb;
        }

        private void ClearBlock(int x, int y)
        {
            var offset = (y * 32) + x;
            _room.blocks[offset] = 0;
        }

        private void DoWillyLeft()
        {
            if (dir == 0)
            {
                dir = 1;
            }
            else
            {
                _x -= 2;

                var block1 = GetBlock(_x, _y);
                var block2 = GetBlock(_x, _y + 8);
                var block3 = GetBlock(_x, _y + 12);

                if (block1 == 3 || block2 == 3 || block3 == 3)
                {
                    _x += 2;
                }

                var blockId = dir == 1 ? (8 + ((_x & 15) >> 1)) : (_x & 15) >> 1;
                SetFrame(blockId);

                Position = new Vector2(_x & 248, _y);
            }
        }

        private void DoWillyRight()
        {
            if (dir == 1)
            {
                dir = 0;
            }
            else
            {
                _x += 2;

                var block1 = GetBlock(_x + 8, _y);
                var block2 = GetBlock(_x + 8, _y + 8);
                var block3 = GetBlock(_x + 8, _y + 12);

                if (block1 == 3 || block2 == 3 || block3 == 3)
                {
                    _x -= 2;
                }

                var blockId = dir == 1 ? (8 + ((_x & 15) >> 1)) : (_x & 15) >> 1;
                SetFrame(blockId);

                Position = new Vector2(_x & 248, _y);
            }
        }

        private void DoWillyJump()
        {
            _jp = ((_j & 254) - 8) / 2;
            _y = _y + _jp;

            if (_j < 8)
            {
                var block1 = GetBlock(_x, _y);
                var block2 = GetBlock(_x + 8, _y);

                if (block1 == 3 || block2 == 3)
                {
                    _state = 4;
                    _js = 0;
                    _y = (_y + 8) & 248;
                }
            }

            if (_j > 11)
            {
                if ((_y & 7) == 0)
                {
                    var block1 = GetBlock(_x, _y + 16);
                    var block2 = GetBlock(_x + 8, _y + 16);

                    if (block1 != 0 || block2 != 0)
                    {
                        _state = 0;
                        _j = 0;
                        _y = _y & 248;
                    }
                }
            }
            _j++;

            if (_j == 18)
            {
                _state = 0;
                _j = 0;
                CheckWillyFall();
            }

            if (_j < 11)
            {
                _js++;
            }
            else
            {
                if (_j > 10)
                {
                    _js--;
                }
            }


            if (_j > 12)
            {
                _willyFall = _willyFall + _jp;
            }

            if (_state != 0)
            {
                Jumping.Invoke(this, _js);
            }
        }

        private void DoNormalMovement(int input)
        {
            CheckCrumb();
            
            if (input == 1)
            {
                DoWillyLeft();
                _willyFall = 0;
            }
            else
            {
                if (input == 2)
                {
                    DoWillyRight();
                    _willyFall = 0;
                }
                else
                {
                    if (input == 4)
                    {
                        _state = 1;
                        _j = 0;
                        _js = 0;
                        _willyFall = 0;
                        DoWillyJump();
                    }
                    else
                    {
                        if (input == 5)
                        {
                            if (dir == 0)
                            {
                                dir = 1;
                                _state = 1;
                                _j = 0;
                                _js = 0;
                                _willyFall = 0;
                                DoWillyJump();
                            }
                            else
                            {
                                _state = 2;
                                _j = 0;
                                _js = 0;
                                _willyFall = 0;
                                DoWillyLeft();
                                DoWillyJump();
                            }
                        }
                        else
                        {
                            if (input == 6)
                            {
                                if (dir == 1)
                                {
                                    dir = 0;
                                    _state = 1;
                                    _j = 0;
                                    _js = 0;
                                    _willyFall = 0;
                                    DoWillyJump();
                                }
                                else
                                {
                                    _state = 3;
                                    _j = 0;
                                    _js = 0;
                                    _willyFall = 0;
                                    DoWillyRight();
                                    DoWillyJump();
                                }
                            }
                            else
                            {
                                _js = 0;
                                _willyFall = 0;
                            }
                        }
                    }
                }
            }
        }

        protected override void OnUpdate(float deltaTime)
        {
            _time += deltaTime;
            if (_time < 0.1f)
            {
                return;
            }

            _time -= 0.1f;
            _input = GetWillyInput();

            CheckRoboHit();

            if (CheckWillyKillBlock() != 0)
            {
                _state = 6;
            }

            if (_state == 0)
            {
                CheckWillyFall();
                CheckWillyConv();
            }

            switch(_state)
            {
                case 0:
                    DoNormalMovement(_input);
                    break;
                case 1:
                    DoWillyJump();
                    break;
                case 2:
                    DoWillyLeft();
                    DoWillyJump();
                    break;
                case 3:
                    DoWillyRight();
                    DoWillyJump();
                    break;
                case 4:
                    DoWillyFall();
                    break;
                case 6:
                    DoDeath();
                    break;
            }

            CheckKeys();
            CheckExit();
            CheckSwitches();
        }

        private void CheckSwitches()
        {
            
        }

        private void CheckExit()
        {
            var exit = new Rectangle(_room.exitPosition.x, _room.exitPosition.y, 16, 16);
            var player = new Rectangle(_x + 4, _y + 8, 2, 2);
            if (exit.Intersects(player))
            {
                if (_roomId == 20 && _cheat == 0)
                {
                    // GAMEmode = 0
                    // LASTm = 0
                }
                else
                {
                    _changeGameState(GameStateType.LevelDone);
                }
            }


    //        If cEXITs = 1

    //    If RectsOverlap(cEXITx, cEXITy,16,16,cWILLYx + 4,cWILLYy + 8,2,2)= 1

    //        If ROOM = 20 And CHEAT = 0

    //            GAMEmode = 6

    //            LASTm = 0

    //        Else
    //            GAMEmode = 3

    //        End If

    //    End If

    //End If
        }

        private void CheckKeys()
        {
            var keys = new List<MMPoint>(_room.keys);
            var i = 0;
            while (i < keys.Count)
            {
                var kx = keys[i].x;
                var ky = keys[i].y;

                var keyRect = new Rectangle(kx, ky, 8, 8);
                var playerRect = new Rectangle(_x, _y, 10, 18);

                if (keyRect.Intersects(playerRect))
                {
                    keys.RemoveAt(i);
                    IncrementScore?.Invoke(this, 100);
                }
                else
                {
                    i++;
                }
            }
            _room.keys = keys.ToArray();
        }

        private void DoDeath()
        {
            //GAMEmode = 2
            //DEATHm = 0
            //DEATHc = 0
            //DEATHi = CopyImage(imageGAME)
            //If ChannelPlaying(MMusic)
            //StopChannel MMusic
            //End If
            //PlaySound SFXdie

            Reset();
            OnDeath?.Invoke(this, EventArgs.Empty);
        }

        private void DoWillyFall()
        {
            //          cWILLYy = cWILLYy + 4
            _y += 4;
            //  blk1 = GetBlock(cWILLYx, cWILLYy + 16)
            var block1 = GetBlock(_x, _y + 16);
            //  blk2 = GetBlock(cWILLYx + 8, cWILLYy + 16)
            var block2 = GetBlock(_x + 8, _y + 16);

            //  If blk1<>0 Or blk2<>0
            if (block1 != 0 || block2 != 0)
            {
                //      cWILLYy = (cWILLYy And 248)
                _y &= 248;
   
                //cWILLYm = 0
                _state = 0;
                //      If cWILLYfall>= 32
                if (_willyFall >= 32)
                {
                    //          cWILLYm = 6
                    _state = 6;
                }
                else //      Else
                {
                    //          cWILLYfall = 0
                    _willyFall = 0;
                }
                //      End If


            }
            else
            {
                //  Else
                //      cWILLYfall = cWILLYfall + 4
                _willyFall += 4;
                //  End If
            }
            //  cWILLYjs = (cWILLYjs + 1) Mod 11
            _js = (_js + 1) % 11;

            //  SoundPitch SFXjump,16384 - (cWILLYjs * 1000)
            Position = new Vector2(_x & 248, _y);

            Jumping.Invoke(this, _js);
        }
    }
}
