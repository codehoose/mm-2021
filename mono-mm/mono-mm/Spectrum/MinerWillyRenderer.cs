using System;
using System.Collections.Generic;
using ManicMiner.Converter.Lib.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private int _j;
        private int _js;
        private int _x;
        private int _y;

        private float _time;
        private int _jp;
        private int _score;
        private int _cheat;
        private SoundEffect _pick;

        public event EventHandler<int> ScoreUpdated;

        public MinerWillyRenderer(Texture2D texture, SoundEffect pick)
            : base(texture, 16)
        {
            _pick = pick;
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
            //        blk1 = cCRUMB((cWILLYy / 8) + 3, (cWILLYx / 8) + 1)
            var block1 = GetCrumb(_x / 8, (_y / 8) + 2);
            //blk2 = cCRUMB((cWILLYy / 8) + 3, (cWILLYx / 8) + 2)
            var block2 = GetCrumb(_x / 8 + 1, _y / 8 + 2);

            //If blk1<>0
            if (block1 != 0)
            {
                //    blk1 = blk1 - 1
                block1--;
                SetCrumb(_x / 8, (_y / 8) + 2, block1);
                if (block1 == 0)
                {
                    ClearBlock(_x / 8, (_y / 8) + 2);
                }
                //    cCRUMB((cWILLYy / 8) + 3, (cWILLYx / 8) + 1) = blk1
                //    If blk1 = 0
                //        cROOM((cWILLYy / 8) + 3, (cWILLYx / 8) + 1) = 0
                //    End If
                //End If
            }

            //If blk2<>0
            if (block2 != 0)
            {
                block2--;
                SetCrumb((_x/ 8) + 1, (_y / 8) + 2, block2);
                if (block2 == 0)
                {
                    ClearBlock((_x / 8) + 1, (_y / 8) + 2);
                }
                
                //    blk2 = blk2 - 1
                //    cCRUMB((cWILLYy / 8) + 3, (cWILLYx / 8) + 2) = blk2
                //    If blk2 = 0
                //        cROOM((cWILLYy / 8) + 3, (cWILLYx / 8) + 2) = 0
                //    End If
                //End If
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
                // set music?! to 6
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
                    _score += 100;
                    ScoreUpdated?.Invoke(this, _score);
                    // PlaySound SFXpick
                    _pick.Play();
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
