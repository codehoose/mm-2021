﻿using System;
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
        private int _willyFall;
        private int _state; // cWillym
        private int _j;
        private int _js;
        private int _x;
        private int _y;

        private float _time;
        private int _jp;

        public MinerWillyRenderer(Texture2D texture)
            : base(texture, 16)
        {
            
        }

        public void SetRoom(MMRoom room)
        {
            _room = room;
            var start = _room.willyStart;

            Position = new Vector2(start.pos.x, start.pos.y);
            dir = start.dir;
            _x = start.pos.x;
            _y = start.pos.y;

            var blockId = dir == 1 ? (8 + ((_x & 15) >> 1)) : (_x & 15) >> 1;
            SetFrame(blockId);
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

        }

        private int CheckWillyKillBlock()
        {
            return 0;
        }


        private void CheckWillyConv()
        {
            
        }

        private void CheckWillyFall()
        {
            //        blk1 = GetBlock(cWILLYx, cWILLYy + 16)
            var block1 = GetBlock(_x, _y + 16);
            //blk2 = GetBlock(cWILLYx + 8, cWILLYy + 16)
            var block2 = GetBlock(_x + 8, _y + 16);

    //If blk1 = 0 And blk2 = 0
            if (block1 == 0 && block2 == 0)
            {
                //    cWILLYm = 4
                _state = 4;
                //    cWILLYjs = 0
                _js = 0;
            }


            //End If
        }

        private void CheckCrumb()
        {

        }

        private int GetBlock(int x, int y)
        {
            var roomBlocks = _room.blocks;
            var offset = ((y / 8) * 32) + (x / 8);
            return roomBlocks[offset];
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
            //         jp = ((cWILLYj And 254)-8)/ 2
            _jp = ((_j & 254) - 8) / 2;
            // cWILLYy = (cWILLYy + jp)
            _y = _y + _jp;

            // If cWILLYj<8
            if (_j < 8)
            {
                //     blk1 = GetBlock(cWILLYx, cWILLYy)
                var block1 = GetBlock(_x, _y);
                //     blk2 = GetBlock(cWILLYx + 8, cWILLYy)

                var block2 = GetBlock(_x + 8, _y);

                //     If blk1 = 3 Or blk2 = 3
                if (block1 == 3 || block2 == 3)
                {
                    //         cWILLYm = 4
                    _state = 4;

                    //         cWILLYjs = 0
                    _js = 0;
                    
                    //         cWILLYy = (cWILLYy + 8) And 248
                    _y = (_y + 8) & 248;
                    //     End If
                }
                // End If
            }

            // If cWILLYj> 11
            if (_j > 11)
            {
                //     If(cWILLYy And 7) = 0
                if ((_y & 7) == 0)
                {
                    //         blk1 = GetBlock(cWILLYx, cWILLYy + 16)
                    var block1 = GetBlock(_x, _y + 16);
                    //         blk2 = GetBlock(cWILLYx + 8, cWILLYy + 16)
                    var block2 = GetBlock(_x + 8, _y + 16);
                    //         If blk1<>0 Or blk2<>0
                    if (block1 != 0 || block2 != 0)
                    {
                        _state = 0;
                        _j = 0;
                        _y = _y & 248;
                        //             cWILLYm = 0

                        //             cWILLYj = 0

                        //             cWILLYy = (cWILLYy And 248)
                        //End If
                    }
                }
                //     End If

                // End If
            }


            // cWILLYj = cWILLYj + 1
            _j++;

            // If cWILLYj = 18
            if (_j == 18)
            {
                //     cWILLYm = 0
                _state = 0;
                //     cWILLYj = 0
                _j = 0;
                //     CheckWillyFall()
                CheckWillyFall();
                // End If
            }


            // If cWILLYj<11
            if (_j < 11)
            {
                //     cWILLYjs = cWILLYjs + 1
                _js++;
            }
            else
            // Else
            {
                //     If cWILLYj > 10
                if (_j > 10)
                {
                    //         cWILLYjs = cWILLYjs - 1
                    _js--;
                }
                //     End If
            }
            // End If


            // If cWILLYj> 12
            if (_j > 12)
            {
                //     cWILLYfall = (cWILLYfall + jp)
                _willyFall = _willyFall + _jp;
            }
            // End If


            // If cWILLYm<>0
            if (_state != 0)
            {
                //     SoundPitch SFXjump,16384 + (cWILLYjs * 1500)

                //     PlaySound SFXjump
            }
            // End If
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

        public override void Update(float deltaTime)
        {
            _time += deltaTime;
            if (_time < 0.1f)
            {
                return;
            }

            _time -= 0.1f;

            var input = GetWillyInput();

            CheckRoboHit();

            if (CheckWillyKillBlock() != 0)
            {
                // set music to 6
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
                    DoNormalMovement(input);
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
                //		Case	4
                //			DoWillyFall()
                //		Case	6
                //			DoDeath()

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
         
        }

        private void CheckKeys()
        {
            
        }

        private void DoDeath()
        {
            
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

          //  PlaySound SFXjump
        }
    }
}
