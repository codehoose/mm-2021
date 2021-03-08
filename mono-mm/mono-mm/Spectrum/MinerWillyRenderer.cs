using System;
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
        private MMMapFile _mapFile;
        private int _room;
        private int _willyFall;
        private int _state; // cWillym
        private int _j;
        private int _js;
        private int _x;
        private int _y;

        private float _time;

        public MinerWillyRenderer(Texture2D texture)
            : base(texture, 16)
        {
            
        }

        public void Init(MMMapFile mapFile, int room)
        {
            _mapFile = mapFile;
            _room = room;

            var start = _mapFile.rooms[room].willyStart;

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
            
        }

        private void CheckCrumb()
        {

        }

        private int GetBlock(int x, int y)
        {
            var roomBlocks = _mapFile.rooms[_room].blocks;
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
            }
        }

        //	Select cWILLYm
        //		Case	0
        //			CheckCrumb()
        //;---------------------------------------
        //			If INKEY = 1
        //				DoWillyLeft()
        //				cWILLYfall=0
        //			Else
        //;---------------------------------------
        //				If INKEY = 2
        //					DoWillyRight()
        //					cWILLYfall=0
        //				Else
        //;---------------------------------------
        //					If INKEY = 4
        //						cWILLYm=1
        //						cWILLYj=0
        //						cWILLYjs=0
        //						cWILLYfall=0
        //						DoWillyJump()
        //					Else
        //;---------------------------------------
        //						If INKEY = 5
        //							If cWILLYd = 0
        //								cWILLYd=1
        //								cWILLYm=1
        //								cWILLYj=0
        //								cWILLYjs=0
        //								cWILLYfall=0
        //								DoWillyJump()
        //							Else
        //								cWILLYm = 2
        //								cWILLYj=0
        //								cWILLYjs=0
        //								cWILLYfall=0
        //								DoWillyLeft()
        //								DoWillyJump()
        //							End If
        //						Else
        //;---------------------------------------
        //							If INKEY = 6
        //								If cWILLYd = 1
        //									cWILLYd=0
        //									cWILLYm=1
        //									cWILLYj=0
        //									cWILLYjs=0
        //									cWILLYfall=0
        //									DoWillyJump()
        //								Else
        //									cWILLYm = 3
        //									cWILLYj=0
        //									cWILLYjs=0
        //									cWILLYfall=0
        //									DoWillyRight()
        //									DoWillyJump()
        //								End If
        //;---------------------------------------
        //							Else
        //									cWILLYjs = 0
        //									cWILLYfall=0
        //							End If
        //						End If
        //					End If
        //				End If
        //			End If
        //		Case	1
        //			DoWillyJump()
        //		Case	2
        //			DoWillyLeft()
        //			DoWillyJump()
        //		Case	3
        //			DoWillyRight()
        //			DoWillyJump()
        //		Case	4
        //			DoWillyFall()
        //		Case	6
        //			DoDeath()
        //	End Select
        //	CheckKeys()
        //	CheckExit()
        //	CheckSwitches()
        //	DrawWilly()
        //End Function



    }
}
