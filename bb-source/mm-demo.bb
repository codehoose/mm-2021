;///////////////////////////////////////
;//	Run Demo
;///////////////////////////////////////
Function	DoDemo()
	Select	DEMOm
		Case	0
			SetupDemo()
		Case	1
			WaitVR()
			SwapPage()

			DrawBlock(imageROOM,0,0)
			DrawCrumb()
			DrawKeys()
			DrawSwitches()
			DrawConv()
			DoHrobo()
			DoVrobo()
			DoSpecialRobo()
			DrawExit()

			DrawAir()
			DrawLives()

			DEMOp=DEMOp+1
			If DEMOp=60
				DEMOp=0
				DEMOm=2
				DEMOi=CopyImage(imageGAME)
			End If
			tim2=tim1
		Case	2
			WaitVR()
			SwapPage()

			DEMOp=DEMOp+1
			If DEMOp=64
				DEMOp=0
				DEMOm=3
			End If
			If (DEMOp And 3) <2
				Color 200,200,200
				Rect 0,0,256,128,True
			Else		
				DrawBlock(DEMOi,0,0)
			End If
		Case	3
			ROOM=ROOM+1
			If ROOM=21
				ROOM=1
			End If
			SetupDemo2()
			DEMOm=1
	End Select
	If GetKey()<>0
		MODE=0
		TITLEm=0
		StopChannel MMusic
	End If
End Function
;///////////////////////////////////////
;//	Setup Demo
;///////////////////////////////////////
Function	SetupDemo()
	FlushKeys()
	ROOM=1
	SetupDemo2()
	DEMOm=1
	DEMOp=0
	If MUSICon=1
		MMusic=PlayMusic("data\sfx\ingame.xm")
	End If
End Function
;///////////////////////////////////////
;//	Setup Demo2
;///////////////////////////////////////
Function	SetupDemo2()

	CopyRoom()
	BLOCKoff=(ROOM-1)*16
	BuildMap()

	BufferGame()
	ClsColor 0,0,0
	Cls
	DrawBlock(imageTITLE,0,128,0)
	DrawBlock(imageTITLE,0,136,1)
	SpeccyPrint(0,16*8,cTITLE$,0)
	DrawAir()
	DrawLives()

	SetBuffer(BackBuffer())
	ClsColor 0,0,0
	Cls
	Border(cBORDER)
	DrawBlock(imageROOM,0,0)
	DrawCrumb()
	DrawKeys()
	DrawSwitches()
	DrawConv()
	DoHrobo()
	DoVrobo()
	DoSpecialRobo()
	DrawExit()
	SwapPage()

	SetBuffer(BackBuffer())
	ClsColor 0,0,0
	Cls
	Border(cBORDER)
	DrawBlock(imageROOM,0,0)
	DrawCrumb()
	DrawKeys()
	DrawSwitches()
	DrawConv()
	DoHrobo()
	DoVrobo()
	DoSpecialRobo()
	DrawExit()
	SwapPage()

End Function