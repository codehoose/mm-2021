;///////////////////////////////////////
;//	Main Game
;///////////////////////////////////////
Function	DoGame()
	Select	GAMEmode
		Case	0
			SetupGame()
		Case	1
			WaitVR()
			SwapPage()
			If PAUSE=0
				DrawBlock(imageROOM,0,0)
				DrawCrumb()
				DrawKeys()
				DrawSwitches()
				DrawConv()
				DoHrobo()
				DoVrobo()
				DoWilly()
				DoSpecialRobo()
				DrawExit()

				DrawAir()
				DoExtraLife()
				DrawLives()
				PrintScore()
				PrintHiScore()

				CheckMusic()
				If CHEAT=0
					CheckCheat()
				Else
					DoCheat()
				End If

				If KeyDown(1)
					GAMEmode=5
					SUREm=0
					SUREs=0
				End If
			Else
				DrawBlock(PAUSEim,0,0)
				SpeccyPrint(PAUSEx-1,PAUSEy,"PAUSED",0)
				SpeccyPrint(PAUSEx+1,PAUSEy,"PAUSED",0)
				SpeccyPrint(PAUSEx,PAUSEy-1,"PAUSED",0)
				SpeccyPrint(PAUSEx,PAUSEy-1,"PAUSED",0)

				SpeccyPrint(PAUSEx,PAUSEy,"PAUSED",(PAUSEi/2)+1)
				PAUSEi=(PAUSEi+1) Mod 12
				PAUSEy=( 64+Sin(PAUSEr)*16)
				PAUSEr=(PAUSEr+10) Mod 360
			End If
			CheckPause()

		Case	2
			Killed()
		Case	3
			LevelDone()
		Case	4
			GameOver()
		Case	5
			WaitVR()
			SwapPage()
			AreYouSure()
		Case	6
			LastExit()
	End Select
	tim2=tim1

End Function
;///////////////////////////////////////
;//	Setup Game for FIRST Play
;///////////////////////////////////////
Function	SetupGame()
	fps=MilliSecs();****

	FlushKeys()

	ROOM=1

	SCORE=0
	
	LIVES=3
	LIVESp=0
	LIVESf=0

	EXTRA=10000
	EXTRAdelta=10000

	CopyRoom()
	BLOCKoff=(ROOM-1)*16
	BuildMap()

	BufferGame()
	ClsColor 0,0,0
	Cls
	DrawBlock(imageTITLE,0,128,0)
	DrawBlock(imageTITLE,0,136,1)
	SpeccyPrint(0,16*8,cTITLE$,0)
	SpeccyPrint2(0,19*8,"High Score          Score",6)
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
	DoWilly()
	DoSpecialRobo()
	DrawExit()
	DrawAir()
	DoExtraLife()
	DrawLives()
	PrintScore()
	PrintHiScore()
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
	DoWilly()
	DoSpecialRobo()
	DrawExit()
	DrawAir()
	DoExtraLife()
	DrawLives()
	PrintScore()
	PrintHiScore()
	SwapPage()

	GAMEmode=1

	EXTRAm=0

	MUSICh=1
	PAUSE=0
	PAUSEh=0
	
	If MUSICon=1
		MMusic=PlayMusic("data\sfx\ingame.xm")
	End If
End Function
;///////////////////////////////////////
;//	Copy Room into Current Room
;///////////////////////////////////////
Function	CopyRoom()

	For y=1 To 16
		For x=1 To 32
			cROOM(y,x)=ROOMS(ROOM,y,x)
			If cROOM(y,x)=4
				cCRUMB(y,x)=8
			Else
				cCRUMB(y,x)=0
			End If
		Next
	Next

	For x=1 To 5
		cKEYSx(x)=KEYx(ROOM,x)
		cKEYSy(x)=KEYy(ROOM,x)
		cKEYSs(x)=KEYs(ROOM,x)
		cKEYSb(x)=(x-1)
		cKEYSbp(x)=0
	Next
	
	For x=1 To 2
		cSWITCHx(x)=SWITCHx(ROOM,x)
		cSWITCHy(x)=SWITCHy(ROOM,x)
		cSWITCHs(x)=SWITCHs(ROOM,x)
	Next
		
	cCONVx=CONVx(ROOM)
	cCONVy=CONVy(ROOM)
	cCONVd=CONVd(ROOM)
	cCONVl=CONVl(ROOM)
	cCONVanim=0
	
	cTITLE$=TITLES$(ROOM)
	cBORDER=BORDERS(ROOM)
	cAIR=AIR(ROOM)+31	
	cAIRp=8

	cEXITx=EXITx(ROOM)
	cEXITy=EXITy(ROOM)
	cEXITs=0
	cEXITanim=0

	For x=1 To 4
		cHROBOx(x)=HROBOx(ROOM,x)
		cHROBOy(x)=HROBOy(ROOM,x)
		cHROBOmin(x)=HROBOmin(ROOM,x)
		cHROBOmax(x)=HROBOmax(ROOM,x)
		cHROBOd(x)=HROBOd(ROOM,x)
		cHROBOs(x)=HROBOs(ROOM,x)
		cHROBOgfx(x)=HROBOgfx(ROOM,x)
		cHROBOflip(x)=HROBOflip(ROOM,x)
		cHROBOanim(x)=HROBOanim(ROOM,x)
	Next

	For	x=1 To 4
		cVROBOx(x)=VROBOx(ROOM,x)
		cVROBOy(x)=VROBOy(ROOM,x)
		cVROBOmin(x)=VROBOmin(ROOM,x)
		cVROBOmax(x)=VROBOmax(ROOM,x)
		cVROBOd(x)=VROBOd(ROOM,x)
		cVROBOs(x)=VROBOs(ROOM,x)
		cVROBOgfx(x)=VROBOgfx(ROOM,x)
		cVROBOanim(x)=x
	Next

	EUGENEx=120
	EUGENEy=1
	EUGENEd=0
	EUGENEm=0
	EUGENEc=7
	EUGENEmin=1
	EUGENEmax=87

	KONGx=120
	KONGy=0
	KONGm=0
	KONGf=0
	KONGp=0

	HOLEl=2
	HOLEy=95
	
	cSWITCH1m=0
	cSWITCH2m=0

	SKYp(1)=1
	SKYp(2)=3
	SKYp(3)=2

	SKYs(1)=4;
	SKYs(2)=3;
	SKYs(3)=1;

	SKYmax(1)=72;
	SKYmax(2)=56;
	SKYmax(3)=32;

	For x=1 To 3
		SKYx(x)=SKYpx(x,SKYp(x))
		SKYy(x)=SKYpy(x,SKYp(x))
		SKYm(x)=0
		SKYf(x)=0
	Next

	SPGf=0
	
	cWILLYx=WILLYsx(ROOM)
	cWILLYy=WILLYsy(ROOM)
	cWILLYd=WILLYsd(ROOM)
	cWILLYm=0
	cWILLYfall=0	
	cWILLYj=0	
	cWILLYjs=0	

End Function
;///////////////////////////////////////
;//	Build Map
;///////////////////////////////////////
Function	BuildMap()
	BufferRoom()
	For y=0 To 15
		For x=0 To 31
			dat=cROOM(y+1,x+1)
			If dat=4
				DrawBlock(imageBLOCKS,x*8,y*8,BLOCKoff)
			Else
				DrawBlock(imageBLOCKS,x*8,y*8,BLOCKoff+dat)
			End If
		Next
	Next
	If ROOM=20
		DrawImage(imageBG,0,0)
		DrawImage(imageSUN,60,32)
	End If
	BufferGame()
End Function
;///////////////////////////////////////
;//	Draw Crumbling Blocks
;///////////////////////////////////////
Function	DrawCrumb()
	For y=0 To 15
		For x=0 To 31
			dat=cCRUMB(y+1,x+1)
			If dat<>0
				SetBuffer(ImageBuffer(imageCRUMB))
				DrawBlock(imageBLOCKS,0,0,BLOCKoff)
				DrawBlock(imageBLOCKS,0,(8-dat),BLOCKoff+4)
				SetBuffer(ImageBuffer(imageGAME))
				DrawBlock(imageCRUMB,x*8,y*8)
			End If
		Next
	Next
End Function
;///////////////////////////////////////
;//	Draw keys
;///////////////////////////////////////
Function	DrawKeys()
	count=0
	For i=1 To 5
		If cKEYSs(i)=1
			DrawImage(imageBLOCKS,cKEYSx(i),cKEYSy(i),(BLOCKoff+11)+cKEYSb(i))
			count=count+1
			cKEYSbp(i)=cKEYSbp(i)+1
			If cKEYSbp(i)=2
				cKEYSb(i)=(cKEYSb(i)+1) And 3
				cKEYSbp(i)=0
			End If
		End If
	Next
	If count=0
		cEXITs=1
	End If
End Function
;///////////////////////////////////////
;//	Draw Switches
;///////////////////////////////////////
Function	DrawSwitches()
	For i=1 To 2
		If cSWITCHs(i)<>0
			DrawBlock(imageSWITCH,cSWITCHx(i),cSWITCHy(i),cSWITCHs(i)-1)
		End If
	Next
End Function
;///////////////////////////////////////
;//	Draw Conv
;///////////////////////////////////////
Function	DrawConv()
	If cCONVl>0
		For x=0 To (cCONVl-1)
			DrawBlock(imageBLOCKS,cCONVx+(x*8),cCONVy,(BLOCKoff+7)+cCONVanim)
		Next
		If Not cCONVd
			cCONVanim=(cCONVanim+1) And 3
		Else
			cCONVanim=(cCONVanim-1) And 3
		End If			
	End If
End Function
;///////////////////////////////////////
;//	Draw Exit
;///////////////////////////////////////
Function DrawExit()
	If cEXITs=0
		DrawBlock(image16,cEXITx,cEXITy,419+ROOM)
	Else
		If cEXITanim<8
			DrawBlock(image16,cEXITx,cEXITy,439+ROOM)
		Else
			DrawBlock(image16,cEXITx,cEXITy,419+ROOM)
		End If
		cEXITanim=(cEXITanim+1)And 15
	End If
End Function
;///////////////////////////////////////
;//	Draw Air
;///////////////////////////////////////
Function	DrawAir()
	DrawBlock(imageTITLE,0,136,1)

	Color 110,110,110
	Line 32,138,cAIR,138
	Color 210,210,210
	Line 32,139,cAIR,139
	Color 160,160,160
	Line 32,140,cAIR,140
	Color 110,110,110
	Line 32,141,cAIR,141
	
	Color 80,80,80
	Plot 32,138
	Plot 32,141
	Plot cAIR,138
	Plot cAIR,141
	Color 180,180,180
	Plot 32,139
	Plot cAIR,139
	Color 130,130,130
	Plot 32,140
	Plot cAIR,140
	
	cAIRp=cAIRp-1
	If cAIRp<=0
		cAIRp=8
		cAIR=cAIR-1
		If cAIR=32
			;cAIR=AIR(ROOM)	;****BODGE****
			cWILLYm=6
		End If
	End If
End Function
;///////////////////////////////////////
;//	Draw Lives
;///////////////////////////////////////
Function	DrawLives()
	count=LIVES

	If count>8
		count=8
	End If	
	
	For x=1 To count-1
		DrawBlock(image16,(x-1)*16,168,LIVESf)
	Next
	If ChannelPlaying(MMusic)
		LIVESp=Livesp+1
		If LIVESp>3
			LIVESp=0
			LIVESf=(LIVESf+1) And 3
		End If
	End If
	If CHEAT=1
		DrawBlock(image16,(x-1)*16,168,461)
	End If
End Function
;///////////////////////////////////////
;//	Do Horizontal Robot
;///////////////////////////////////////
Function	DoHrobo()
	For	i=1 To 4
		If cHROBOx(i)<>-1
			If cHROBOd(i)
				cHROBOx(i)=(cHROBOx(i)-cHROBOs(i))
				If cHROBOx(i)<=cHROBOmin(i)
					cHROBOd(i)=0
					;cHROBOx(i)=(cHROBOx(i)+cHROBOs(i))
					DrawImage(image16,cHROBOx(i) And 248,cHROBOy(i),cHROBOgfx(i)+((cHROBOx(i) And cHROBOanim(i))/2))
				Else
					DrawImage(image16,cHROBOx(i) And 248,cHROBOy(i),(cHROBOgfx(i)+((cHROBOx(i) And cHROBOanim(i))/2))+cHROBOflip(i))
				End If
			Else
				cHROBOx(i)=(cHROBOx(i)+cHROBOs(i))
				If cHROBOx(i)>cHROBOmax(i)
					cHROBOd(i)=1
					;cHROBOx(i)=(cHROBOx(i)-cHROBOs(i))
					DrawImage(image16,cHROBOx(i) And 248,cHROBOy(i),(cHROBOgfx(i)+((cHROBOx(i) And cHROBOanim(i))/2))+cHROBOflip(i))
				Else
					DrawImage(image16,cHROBOx(i) And 248,cHROBOy(i),cHROBOgfx(i)+((cHROBOx(i) And cHROBOanim(i))/2))
				End If
			End If
		End If
	Next
End Function
;///////////////////////////////////////
;//	Do Vertical Robot
;///////////////////////////////////////
Function	DoVrobo()
	For i=1 To 4
		If cVROBOx(i)<>-1
			If cVROBOd(i)
				cVROBOy(i)=(cVROBOy(i)-cVROBOs(i))
				If cVROBOy(i)<cVROBOmin(i)
					cVROBOd(i)=0
					cVROBOy(i)=(cVROBOy(i)+cVROBOs(i))
					DrawImage(image16,cVROBOx(i),cVROBOy(i),cVROBOgfx(i)+cVROBOanim(i))
				Else
					DrawImage(image16,cVROBOx(i),cVROBOy(i),cVROBOgfx(i)+cVROBOanim(i))
				End If
			Else
				cVROBOy(i)=(cVROBOy(i)+cVROBOs(i))
				If cVROBOy(i)>cVROBOmax(i)
					cVROBOd(i)=1
					cVROBOy(i)=(cVROBOy(i)-cVROBOs(i))
					DrawImage(image16,cVROBOx(i),cVROBOy(i),cVROBOgfx(i)+cVROBOanim(i))
				Else
					DrawImage(image16,cVROBOx(i),cVROBOy(i),cVROBOgfx(i)+cVROBOanim(i))
				End If
			End If
			cVROBOanim(i)=(cVROBOanim(i)+1) And 3
		End If
	Next			
End Function
;///////////////////////////////////////
;//	Do Special Robot
;///////////////////////////////////////
Function	DoSpecialRobo()
	Select	ROOM
		Case	5
			DoEugene()
		Case	8
			DoLevelEight()
		Case	12
			DoLevelEight()
		Case	14
			DoLevelFourteen()
		Case	19
			DoSPG()
			CheckSPG()
	End Select
End Function
;///////////////////////////////////////
;//	Do Eugene
;///////////////////////////////////////
Function	DoEugene()
	Select	EUGENEm
		Case	0
			If EUGENEd
				EUGENEy=EUGENEy-1
				If EUGENEy<EUGENEmin
					EUGENEd=0
					EUGENEy=EUGENEy+1
				End If
				DrawImage(image16,EUGENEx,EUGENEy,418)
			Else
				EUGENEy=EUGENEy+1
				If EUGENEy>EUGENEmax
					EUGENEd=1
					EUGENEy=EUGENEy-1
				End If
				DrawImage(image16,EUGENEx,EUGENEy,418)
			End If
			If cEXITs=1
				EUGENEm=1
				EUGENEc=0
			End If
		Case	1
			EUGENEy=EUGENEy+1
			If EUGENEy>EUGENEmax
				EUGENEy=EUGENEy-1
			End If
			DrawImage(image16,EUGENEx,EUGENEy,412+EUGENEc)
			EUGENEc=(EUGENEc+1) And 7
		End Select
End Function
;///////////////////////////////////////
;//	Do Level 8
;///////////////////////////////////////
Function	DoLevelEight()

	KONGp=KONGp+1
	If KONGp=8
		KONGp=0
		KONGf=(KONGf+1) And 1
	End If

	
	Select cSWITCHs(2)
		Case	1
			DrawImage(image16,KONGx,KONGy,408+KONGf)	
		Case	2
			DoKongFall()
	End Select

	If cSWITCHs(1)=2
		DoHole()
	End If
End Function
;///////////////////////////////////////
;//	Do Knong Fall
;///////////////////////////////////////
Function	DoKongFall()
	Select	KONGm
		Case	0
			cROOM(3,16)=0
			cROOM(3,17)=0
			BufferRoom()
			Color 0,0,0
			Rect 120,16,16,8,True
			BufferGame()
			DrawImage(image16,KONGx,KONGy,408+KONGf)
			KONGm=1
		Case	1
			KONGy=KONGy+4
			SoundPitch SFXjump2,22050-(KONGy*(((KONGp And 1)+1)*50))
			PlaySound SFXjump2
			DrawImage(image16,KONGx,KONGy,410+KONGf)
			SCORE=SCORE+100
			If KONGy>=104
				KONGm=2
			End If
	End Select
End Function
;///////////////////////////////////////
;//	Do Hole
;///////////////////////////////////////
Function	DoHole()
	Select cSWITCH1m
		Case	0
			Color 0,0,0
			Rect 136,HOLEy,8,HOLEl,True
			HOLEy=HOLEy-1
			HOLEl=HOLEl+2
			If HOLEy=87
				cSWITCH1m=1
				cROOM(12,18)=0
				cROOM(13,18)=0
				BufferRoom()
				Color 0,0,0
				Rect 136,88,8,16,True
				BufferGame()
				cHROBOmax(2)=cHROBOmax(2)+24
			End If
	End Select
End Function
;///////////////////////////////////////
;//	Do Level 14
;///////////////////////////////////////
Function DoLevelFourteen()
	For i=1 To 3
		Select SKYm(i)
			Case	0
				SKYy(i)=(SKYy(i)+SKYs(i))
				If SKYy(i)>SKYmax(i)
					SKYy(i)=SKYmax(i)
					SKYm(i)=1
					SKYf(i)=SKYf(i)+1
				End If
				DrawImage(image16,SKYx(i),SKYy(i),(252+(i*8))+SKYf(i))
			Case	1
				SKYf(i)=SKYf(i)+1
				If SKYf(i)=7
					SKYm(i)=2
				End If
				DrawImage(image16,SKYx(i),SKYy(i),(252+(i*8))+SKYf(i))
			Case	2
				SKYp(i)=(SKYp(i)+1) Mod 4
				If SKYp(i)=0
					SKYp(i)=1
				End If
				SKYx(i)=SKYpx(i,SKYp(i))
				SKYy(i)=SKYpy(i,SKYp(i))
				SKYf(i)=0
				SKYm(i)=0
		End Select
	Next		
End Function
;///////////////////////////////////////
;//	Draw SPG Block
;///////////////////////////////////////
Function	SPGblock(x,y)
		LockBuffer (ImageBuffer(imageGAME))
		For ypos=0 To 7
			For xpos=0 To 7
				rgb=ReadPixelFast(x+xpos,y+ypos,ImageBuffer(imageGAME))
				red=((rgb Shr 16) And 255)
				green=((rgb Shr 8) And 255)
				blue=(rgb And 255)
				pix=((red+green+blue)/3)+64
				If pix>255
					pix=255
				End If
				rgb=((pix Shl 16)+(pix Shl 8))
				WritePixelFast x+xpos,y+ypos,rgb,ImageBuffer(imageGAME)
			Next
		Next
		UnlockBuffer (ImageBuffer(imageGAME))
End Function
;///////////////////////////////////////
;//	Do SPG
;///////////////////////////////////////
Function DoSPG()
	FindSPG()
	For i=0 To 64
		If SPGx(i)<>-1
			SPGblock(SPGx(i)*8,SPGy(i)*8)
		End If
	Next
End Function
;///////////////////////////////////////
;//	Find SPG
;///////////////////////////////////////
Function	FindSPG()
	For i=0 To 64
		SPGx(i)=-1
		SPGy(i)=-1
	Next

	i=0
	x=23
	y=0
	dir=0
	done=0
	
	Repeat
		blockhit=cROOM(y+1,x+1)
		robohit=SPGCheckRobo(x,y)
	
		If (blockhit=0) And (robohit=False)
			SPGx(i)=x
			SPGy(i)=y
		Else
			If(blockhit<>0) And (robohit=True)
				SPGx(i)=-1
				SPGy(i)=-1
				done=1
			Else
				If (blockhit=0) And (robohit=True)
					SPGx(i)=x
					SPGy(i)=y
					dir=(dir+1) And 1
				Else
					If (blockhit<>0) And (robohit=False)
						SPGx(i)=-1
						SPGy(i)=-1
						done=1
					End If
				End If
			End If
		End If

		i=i+1
		If i=64
			done=1
		End If
	
		If dir=0
			y=y+1
			If y=16
				done=1
				SPGx(i)=-1
				SPGy(i)=-1
			End If
		Else
			x=x-1
			If x=0
				done=1
				SPGx(i)=-1
				SPGy(i)=-1
			End If
		End If
	Until done<>0
	
End Function
;///////////////////////////////////////
;//	Check if SPG hit Robot
;///////////////////////////////////////
Function	SPGCheckRobo(x,y)

	hit=0

	x=x*8
	y=y*8

	For i=1 To 4
		If cHROBOx(i)<>-1
			If RectsOverlap(x,y,8,8,cHROBOx(i),cHROBOy(i),16,16)=1
				hit=hit+1
			End If
		End If
	Next

	For i=1 To 4
		If cVROBOx(i)<>-1
			If RectsOverlap(x,y,8,8,cVROBOx(i),cVROBOy(i),16,16)=1
				hit=hit+1
			End If
		End If
	Next

	If hit<>0
		Return True
	Else
		Return False
	End If
End Function
;///////////////////////////////////////
;//	Do Willy Killed
;///////////////////////////////////////
Function	Killed()
	If EXTRAm<>0
		LIVES=LIVES+1
		EXTRA=EXTRA+EXTRAdelta
		EXTRAm=0
	End If
	
	WaitVR()
	SwapPage()
	Select	DEATHm
		Case	0
			Color 256-(DEATHc*32),256-(DEATHc*32),256-(DEATHc*32)
			Rect 0,0,256,128,True
			DEATHc=DEATHc+1
			If DEATHc=9
				LIVES=LIVES-1
				If LIVES=0
					GAMEmode=4
					OVERm=0
				End If
				DEATHm=1
			End If
		Case	1
			CopyRoom()
			
			BuildMap()
			DrawBlock(imageROOM,0,0)
			
			DrawCrumb()
			DrawKeys()
			DrawSwitches()
			DrawConv()
			DoHrobo()
			DoVrobo()
			DoWilly()
			DoSpecialRobo()
			DrawExit()
			DrawBlock(imageTITLE,0,136,1)
			DrawAir()
			PrintScore()
			PrintHiScore()
			Color 0,0,0
			Rect 0,168,256,16,True
			DrawLives()
			If ROOM<>20 And MUSICon=1
				MMusic=PlayMusic("data\sfx\ingame.xm")
			Else
				MMusic=PlayMusic("data\sfx\final.xm")
			End If			
			GAMEmode=1
	End Select
End Function
;///////////////////////////////////////
;//	Level Done
;///////////////////////////////////////
Function	LevelDone()
	StopChannel MMusic
	cEXITi=CopyImage(imageGAME)
	
	OLDSPEED=SPEED
	SPEED=2
	For i=0 To 16
		WaitVR()
		SwapPage()
		Color 255,255,255
		Rect(0,0,256,128)
		tim2=tim1
		WaitVR()
		SwapPage()
		DrawBlock(cEXITi,0,0)
		tim2=tim1
	Next
	SPEED=OLDSPEED
	old=0
	Repeat
		SwapPage()
		SoundPitch SFXjump,6000+(cAIR*((old+1)*100))
		PlaySound SFXjump
		SoundPitch SFXjump2,4000+(cAIR*((old+1)*100))
		PlaySound SFXjump2
		DrawAir()
		cAIR=cAIR-1
		old=old Xor 1
		SCORE=SCORE+9
		PrintScore()
		PrintHiScore()
;		DoExtraLife()
;		DrawLives()
	Until cAIR<=32
	ROOM=ROOM+1
	If ROOM=21
		ROOM=1
	End If

	CopyRoom()
	BLOCKoff=(ROOM-1)*16
	BuildMap()
	DrawBlock(imageROOM,0,0)

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
	DoWilly()
	DoSpecialRobo()
	DrawExit()
	DrawAir()
	DoExtraLife()
	DrawLives()
	PrintScore()
	PrintHiScore()
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
	DoWilly()
	DoSpecialRobo()
	DrawExit()
	DrawAir()
	DoExtraLife()
	DrawLives()
	PrintScore()
	PrintHiScore()
	SwapPage()
	
	GAMEmode=1
	
	If ROOM<>20 And MUSICon=1
		MMusic=PlayMusic("data\sfx\ingame.xm")
	Else
		MMusic=PlayMusic("data\sfx\final.xm")
	End If			
End Function
;///////////////////////////////////////
;//	Game Over
;///////////////////////////////////////
Function	GameOver()
	Select	OVERm
		Case	0
			SetupOver()
		Case	1
			SwapPage()
			DoBoot
		Case	2
			SwapPage()
			DoOverText()
	End Select
End Function
;///////////////////////////////////////
;//	Setup Game Over
;///////////////////////////////////////
Function	SetupOver()
	BOOTy=0
	ClsColor 0,0,0
	Cls

	DrawBlock(image16,120,112,460)
	DrawBlock(image16,118,96,3)
	DrawBlock(image16,120,BOOTy,461)

	DrawBlock(imageTITLE,0,128,0)
	DrawBlock(imageTITLE,0,136,1)
	SpeccyPrint(0,16*8,cTITLE$,0)

	SetBuffer(BackBuffer())
	Cls
	SwapPage()

	SetBuffer(BackBuffer())
	Cls
	SwapPage()

	OVERm=1
End Function
;///////////////////////////////////////
;//	Do Boot
;///////////////////////////////////////
Function	DoBoot()
	BOOTy=BOOTy+1
	DrawBlock(image16,120,BOOTy,461)

	SoundPitch SFXjump,4000+((BOOTy*BOOTy)*((BOOTy And 1)+1))
	PlaySound SFXjump

	If BOOTy=96
		OVERm=2
		OVERi=0
		OVERp=0
	End If	
End Function
;///////////////////////////////////////
;//	Do Game Over text
;///////////////////////////////////////
Function	DoOverText()
	ink=OVERi
	SpeccyPrint2(80,48,"G",ink+1)
	ink=(ink+1) Mod 6
	SpeccyPrint2(88,48,"a",ink+1)
	ink=(ink+1) Mod 6
	SpeccyPrint2(96,48,"m",ink+1)
	ink=(ink+1) Mod 6
	SpeccyPrint2(104,48,"e",ink+1)
	ink=(ink+1) Mod 6

	SpeccyPrint2(144,48,"O",ink+1)
	ink=(ink+1) Mod 6
	SpeccyPrint2(152,48,"v",ink+1)
	ink=(ink+1) Mod 6
	SpeccyPrint2(160,48,"e",ink+1)
	ink=(ink+1) Mod 6
	SpeccyPrint2(168,48,"r",ink+1)
	ink=(ink+1) Mod 6

	OVERp=OVERp+1
	If (OVERp And 1)=0
		OVERi=(OVERi+1) Mod 6
	End If
	
	If OVERp>300
		MODE=0
		TITLEm=0
		DEMOm=0
		GAMEmode=0
		OVERm=0
	End If
End Function
;///////////////////////////////////////
;//	Print Score
;///////////////////////////////////////
Function	PrintScore()
	poo$=Str SCORE
	pos=Len(poo$)
	If pos>6
		pos=6
	End If
	SpeccyPrint2(208,152,"000000",6)
	SpeccyPrint2(256-(pos*8),152,Right$(poo$,6),6)

	If SCORE>HISCORE
		HISCORE=SCORE
	End If
	
	If SCORE>EXTRA
		EXTRA=EXTRA+EXTRAdelta
		EXTRAm=1
		EXTRAx=254
	End If
End Function
;///////////////////////////////////////
;//	Print Score
;///////////////////////////////////////
Function	PrintHiScore()
	poo$=Str HISCORE
	pos=Len(poo$)
	If pos>6
		pos=6
	End If
	SpeccyPrint2(88,152,"000000",6)
	SpeccyPrint2(136-(pos*8),152,Right$(poo$,6),6)

End Function
;///////////////////////////////////////
;//	Do Extra Life
;///////////////////////////////////////
Function	DoExtraLife()
	count=LIVES
	If count>8
		count=8
	End If	

	Select EXTRAm
		Case	1
			Color 0,0,0
			Rect 0,168,256,16,True
			DrawBlock(image16,EXTRAx And 248,168,8+((EXTRAx And 15)Shr 1))
			EXTRAx=EXTRAx-2
			If EXTRAx<=((count-1)*16)
				LIVES=LIVES+1
				EXTRAm=0
			End If
	End Select
End Function
;///////////////////////////////////////
;//	Check Music
;///////////////////////////////////////
Function	CheckMusic()
	If KeyHit(35) Or KeyHit(36) Or KeyHit(37) Or KeyHit(38) Or KeyHit(39) Or KeyHit(40) Or KeyHit(41) Or KeyHit(28)
		If MUSICh=0
			MUSICon=(MUSICon Xor 1)
			MUSICh=1
			If MUSICon=0
				StopChannel MMusic
			Else
				If ROOM<>20 And MUSICon=1
					MMusic=PlayMusic("data\sfx\ingame.xm")
				Else
					MMusic=PlayMusic("data\sfx\final.xm")
				End If			
			End If		
		End If
	Else
		MUSICh=0
	End If
End Function
;///////////////////////////////////////
;//	Check Pause
;///////////////////////////////////////
Function	CheckPause()
	If KeyHit(30) Or KeyHit(31) Or KeyHit(32) Or KeyHit(33) Or KeyHit(34)
		If PAUSEh=0
			PAUSE=(PAUSE Xor 1)
			PAUSEh=1
			If PAUSE=1
				PauseChannel MMusic
				PAUSEi=0
				PAUSEx=104
				PAUSEy=60
				PAUSEr=0
				PAUSEim=CopyImage(imageGAME)
			Else
				If MUSICon=1
					ResumeChannel MMusic
				End If
			End If		
		End If
	Else
		PAUSEh=0
	End If
End Function
;///////////////////////////////////////
;//	Are You Sure
;///////////////////////////////////////
Function	AreYouSure()
	INKEY=GetWillyInput()
	Select SUREm	
		Case 0
			PAUSEim=CopyImage(imageGAME)
			DrawBlock(PAUSEim,0,0)
			PauseChannel MMusic
			Color 0,16,32
			Rect 68,48,120,32,True
			Color 0,128,200
			Rect 68,48,120,32,False
			Color 0,0,8
			Rect 69,49,118,30,False
			Color 128,200,255
			Plot 68,48
			Plot 187,48
			Plot 68,79
			Plot 187,79
			SpeccyPrint(76, 56,"ARE YOU SURE?",7)
			SUREs=1
			SpeccyPrint(92,64, "YES",1)
			SpeccyPrint(148,64,"NO",6)
			SUREm=1
			SUREi=0
		Case	1
			If INKEY=1
				SUREs=0
			Else
				If INKEY=2
					SUREs=1
				Else
					If INKEY=4
						If SUREs=0
							MODE=0
							TITLEm=0
							DEMOm=0
							GAMEmode=0
							OVERm=0
						Else
							Repeat
								INKEY=GetWillyInput()
							Until INKEY=0
							GAMEmode=1
							If MUSICon=1
								ResumeChannel MMusic
							End If
							INKEY=0
							PAUSE=0
							DrawBlock(PAUSEim,0,0)
						End If
					Else
						If SUREs=0
							SpeccyPrint(92,64, "YES",(SUREi/2)+1)
							SpeccyPrint(148,64,"NO",1)
						Else
							SpeccyPrint(92,64, "YES",1)
							SpeccyPrint(148,64,"NO",(SUREi/2)+1)
						End If			
					End If
				End If
			End If
		End Select
		SUREi=(SUREi+1)Mod 12
End Function
;///////////////////////////////////////
;//	Check for Cheat
;///////////////////////////////////////
Function	CheckCheat()
	If CHEATh=0
		If KeyDown(CHEATkey(CHEATp))
			CHEATh=1
			CHEATp=CHEATp+1
			If CHEATp=7
				CHEAT=1
				CHEATh=0
			End If
		Else
			If AnyKey()=1
				CHEATp=1
			End If
		End If
	Else
		If AnyKey()=0
			CHEATh=0
		End If
	End If
End Function
;///////////////////////////////////////
;//	Do Cheat
;///////////////////////////////////////
Function	DoCheat()
	If KeyDown(209)
		ROOM=ROOM-1
		If ROOM=0
			ROOM=20
		End If
		StopChannel MMusic
		Repeat
		Until Not KeyDown(209)
		ChangeLevelCheat()
	Else
		If KeyDown(201)
			ROOM=ROOM+1
			If ROOM=21
				ROOM=1
			End If
			StopChannel MMusic
			Repeat
			Until Not KeyDown(201)
			ChangeLevelCheat()
		End If
	End If

	If KeyDown(199)
		LIVES=8
	End If
End Function
;///////////////////////////////////////
;//	Change Level Cheat
;///////////////////////////////////////
Function ChangeLevelCheat()
	CopyRoom()
	BLOCKoff=(ROOM-1)*16
	BuildMap()
	DrawBlock(imageROOM,0,0)

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
	DoWilly()
	DoSpecialRobo()
	DrawExit()
	DrawAir()
	DoExtraLife()
	DrawLives()
	PrintScore()
	PrintHiScore()
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
	DoWilly()
	DoSpecialRobo()
	DrawExit()
	DrawAir()
	DoExtraLife()
	DrawLives()
	PrintScore()
	PrintHiScore()
	SwapPage()
	
	GAMEmode=1
	
	If ROOM<>20 And MUSICon=1
		MMusic=PlayMusic("data\sfx\ingame.xm")
	Else
		MMusic=PlayMusic("data\sfx\final.xm")
	End If			
End Function
;///////////////////////////////////////
;//	Last Exit
;///////////////////////////////////////
Function	LastExit()
	Select	LASTm
		Case	0
			LastFirst()
		Case	1
			LastSetup()
		Case	2
			LastBit()
		End Select

	If KeyDown(1)
		StopChannel MMusic
		SPEED=OLDSPEED
		GOS=0
		MODE=0
		TITLEm=0
		DEMOm=0
		GAMEmode=0
		OVERm=0
	End If
End Function
;///////////////////////////////////////
;//	Setup Game Compleate
;///////////////////////////////////////
Function	LastSetup()
	OLDSPEED=SPEED
	SPEED=2
	GOS=1

	StopChannel MMusic
	
	SetBuffer(BackBuffer())
	ClsColor 0,0,0
	Cls
	BufferGame()
	Cls
	DrawBlock(imageEND,176,0)
	SwapPage()

	SetBuffer(BackBuffer())
	ClsColor 0,0,0
	Cls
	SwapPage()

	LASTm=2
	LASTf=0
	LASTp=0

	TEXTm=0
	TEXTp=0
	TEXTid=0
	TEXTik=0
	
	MMusic=PlayMusic("data\sfx\done.xm")
End Function
;///////////////////////////////////////
;//	Level Done
;///////////////////////////////////////
Function	LastFirst()
	StopChannel MMusic
	cEXITi=CopyImage(imageGAME)
	
	OLDSPEED=SPEED
	SPEED=2
	For i=0 To 16
		WaitVR()
		SwapPage()
		Color 255,255,255
		Rect(0,0,256,128)
		tim2=tim1
		WaitVR()
		SwapPage()
		DrawBlock(cEXITi,0,0)
		tim2=tim1
	Next
	SPEED=OLDSPEED
	old=0
	Repeat
		SwapPage()
		SoundPitch SFXjump,6000+(cAIR*((old+1)*100))
		PlaySound SFXjump
		SoundPitch SFXjump2,4000+(cAIR*((old+1)*100))
		PlaySound SFXjump2
		DrawAir()
		cAIR=cAIR-1
		old=old Xor 1
		SCORE=SCORE+9
		PrintScore()
		PrintHiScore()
	Until cAIR<=32
	ROOM=ROOM+1
	If ROOM=21
		ROOM=1
	End If

	LASTm=1
End Function
;///////////////////////////////////////
;//	Last Bit
;///////////////////////////////////////
Function	LastBit()
	WaitVR()
	SwapPage()

	DrawBlock(image16,184,8,462+LASTf)
	LASTp=LASTp+1
	If LASTp=4
		LASTp=0
		LASTf=(LASTf+1) And 15
	End If

	DoText()

	tim2=tim1
End Function
;///////////////////////////////////////
;//	End Text Stuff
;///////////////////////////////////////
Function	DoText()
	Select	TEXTm
		Case	0
			FadeUpText()
		Case	1
			WaitText()
		Case	2
			FadeDownText()
		End Select
End Function
;///////////////////////////////////////
;//	Fade Up Text
;///////////////////////////////////////
Function	FadeUpText()
	SpeccyPrint2(0,88,LT$(TEXTid),TEXTik)
	SpeccyPrint2(0,96,LT$(TEXTid+1),TEXTik)
	SpeccyPrint2(0,104,LT$(TEXTid+2),TEXTik)
	TEXTp=TEXTp+1
	If TEXTp=2
		TEXTp=0
		TEXTik=TEXTik+1
		If TEXTik=8
			TEXTm=1
			TEXTik=7
		End If
	End If
End Function
;///////////////////////////////////////
;//	Wait a while
;///////////////////////////////////////
Function	WaitText()
	SpeccyPrint2(0,88,LT$(TEXTid),7)
	SpeccyPrint2(0,96,LT$(TEXTid+1),7)
	SpeccyPrint2(0,104,LT$(TEXTid+2),7)
	TEXTp=TEXTp+1
	If TEXTid<72
		If TEXTp=150
			TEXTp=0
			TEXTm=2
		End If
	End If
End Function
;///////////////////////////////////////
;//	Fade Down Text
;///////////////////////////////////////
Function	FadeDownText()
	SpeccyPrint2(0,88,LT$(TEXTid),TEXTik)
	SpeccyPrint2(0,96,LT$(TEXTid+1),TEXTik)
	SpeccyPrint2(0,104,LT$(TEXTid+2),TEXTik)
	TEXTp=TEXTp+1
	If TEXTp=2
		TEXTp=0
		TEXTik=TEXTik-1
		If TEXTik=-1
			TEXTm=0
			TEXTik=0
			TEXTid=TEXTid+3
			If TEXTid>=74
				TEXTid=72
			End If
		End If
	End If
End Function
