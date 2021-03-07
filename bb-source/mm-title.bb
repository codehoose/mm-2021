;///////////////////////////////////////
;//	Main Titles
;///////////////////////////////////////
Function	MainTitles()
	Select	TITLEm
		Case	0
			Border(2)
			TitlesSetup()
			SwapPage()
			Border(2)
			TitlesSetup()
			MMusic=PlayMusic("data\sfx\title.xm")
		Case	1
			SwapPage()
			DoPiano()

			If KeyDown(59)
				TITLEm=4
				PREFSm=0
			End If

			If KeyDown(28) Or KeyDown(156)
				ROOM=1
				MODE=1
				GAMEmode=0
				StopChannel MMusic
			End If
		Case	2
			SwapPage()
			DoTitleScroll()

			If KeyDown(59)
				TITLEm=4
				PREFSm=0
			End If

			If KeyDown(28) Or KeyDown(156)
				ROOM=1
				MODE=1
				GAMEmode=0
				StopChannel MMusic
			End If
		Case	3
			TITLEm=0
			DEMOm=0
			MODE=2
		Case	4
			DoPrefs()
	End Select
	


End Function
;///////////////////////////////////////
;//	Setup Title Page
;///////////////////////////////////////
Function	TitlesSetup()
	FlushKeys()

	ClsColor	0,0,0
	Cls
	
	DrawBlock(imageBG,0,0)
	DrawImage(imageBGf1,152,40)
	DrawImage(imageBGf2,176,40)
	DrawImage(imageSUN,60,32)
	DrawBlock(imagePIANO,0,64)
	DrawImage(image16,232,72,2)
	DrawBlock(imageTITLE,0,128,0)
	DrawBlock(imageTITLE,0,136,1)
	SpeccyPrint(8*8,16*8,"2001 Andy Noble",0)

	TITLEi=0
	SpeccyPrint2(48,176,"Press F1 for Options",TITLEi+1)

	TITLEm=1
	TITLEc=0
End Function
;///////////////////////////////////////
;//	Do Piano
;///////////////////////////////////////
Function DoPiano()
	TITLEc=TITLEc+1
;	If Not ChannelPlaying(MMusic)
	If TITLEc=1700
		SCROLLpos=0
		SCROLLpixel=0
		SCROLLwf=2
		TITLEm=2
	End If
	SpeccyPrint2(48,176,"Press F1 for Options",TITLEi+1)
	SpeccyPrint3(72,31,"PRESS `fF10`g TO QUIT TO WINDOWS",7)
	If (TITLEc And 7)=0
		TITLEi=(TITLEi+1) Mod 6
	End If
End Function
;///////////////////////////////////////
;//	Do Title Scroll
;///////////////////////////////////////
Function DoTitleScroll()
	scroll$="                                  .  .  .  .  .  .  .  .  .  . MANIC MINER . .  BUG-BYTE ltd. 1983 . . By Matthew Smith . . . Q to P = Left & Right . . Bottom row = Jump . . A to G = Pause . . H to L = Tune On/Off . . . Guide Miner Willy through 20 lethal caverns   .  .  .  .  .  .  .  .                                        Å"

	SCROLLwp=SCROLLwp+1
	If SCROLLwp=16
		SCROLLwp=0
		SCROLLwf=(SCROLLwf+2)And 7
	End If
		
	DrawBlock(imagePIANO,0,64)
	DrawImage(image16,232,72,SCROLLwf+4)

	s$=Mid$(scroll$,SCROLLpos+1,33)
	If Asc(Right(s$,1))=129
		SCROLLpos=0
		TITLEm=3
	Else
		SpeccyPrint2(0-SCROLLpixel,19*8,s$,6)
		SpeccyPrint2(48,176,"Press F1 for Options",TITLEi+1)
		SpeccyPrint3(72,31,"PRESS `fF10`g TO QUIT TO WINDOWS",7)
	End If
	
	SCROLLpixel=SCROLLpixel+1
	If SCROLLpixel=8
		SCROLLpixel=0
		SCROLLpos=SCROLLpos+1
		TITLEi=(TITLEi+1) Mod 6

;		If SCROLLpos=290
;			SCROLLpos=0
;			TITLEm=3
;		End If
	End If
End Function
;///////////////////////////////////////
;//	Do Loading Anim
;///////////////////////////////////////
Function	Load()
	Select	LOADm
		Case	0
			LoadSetup()
		Case	1
			Flip
			If	GetKey()<>0
				DrawBlock(imageLOGO,68,0)
				SpeccyPrint3(300,38,Version$,7)
				Flip
				DrawBlock(imageLOGO,68,0)
				SpeccyPrint3(300,38,Version$,7)
				LOGOy=0
				LOGOacc=4
				LOADm=2
			Else
				LoadAnim()
			End If
		Case	2
			Flip
			LOADc=LOADc+1
			If LOADc>512
				MODE=0
			End If
			If	GetKey()<>0
				If KeyDown(28)=0 And KeyDown(156)=0
					MODE=0
				End If
			End If
	End Select
End Function
;///////////////////////////////////////
;//	Loading Screen Setup
;///////////////////////////////////////
Function	LoadSetup()
	FlushKeys()

	SetBuffer(BackBuffer())
	ClsColor 0,0,0
	Cls
	Flip
	Cls

	LOADm=1
	LOADc=0

	LOGOy=-240
	LOGOthrust=0
	LOGOacc=0.5

	SoundPitch SFXjump2,22050

End Function
;///////////////////////////////////////
;//	Load Anim
;///////////////////////////////////////
Function	LoadAnim()
	tim2=tim1
	Cls
	DrawBlock(imageLOGO,68,LOGOy)

	If LOGOthrust<16
		LOGOthrust=LOGOthrust+LOGOacc
	End If

	LOGOy=LOGOy+LOGOthrust

	If LOGOy>=0
		LOGOy=0
		LOGOthrust=(-LOGOthrust)
		LOGOacc=LOGOacc+0.1
		PlaySound SFXjump
		If LOGOacc>3
			LOADm=2
			SpeccyPrint3(300,38,Version$,7)
			Flip
			SpeccyPrint3(300,38,Version$,7)
		End If
	End If
End Function
;///////////////////////////////////////
;//	Options
;///////////////////////////////////////
Function	DoPrefs()
	Select PREFSm
		Case	0
			PrefsSetup()
		Case	1
			UpdatePrefs()
	End Select
End Function
;///////////////////////////////////////
;//	Setup Prefs Page
;///////////////////////////////////////
Function	PrefsSetup()
	StopChannel MMusic
	
	ClsColor 0,0,0
	SetBuffer(BackBuffer())
	Cls
	BufferGame()
	Cls
	PrefsText()
	Flip
	SetBuffer(BackBuffer())
	Cls
	Flip
	BufferGame()
	PREFSs=SPEED
	OLDSPEED=SPEED
	GOS=1
	SPEED=1
	PREFSm=1
	PREFSh=0
	MMusic=PlayMusic("data\sfx\options.xm")
End Function
;///////////////////////////////////////
;//	Prefs Text
;///////////////////////////////////////
Function	PrefsText()
	Color 255,255,255

	SpeccyPrint3(106, 0,"`bM`fA`dN`eI`cC `eM`cI`bN`fE`dR",2)
	SpeccyPrint3( 94, 1,"@ 2001 Andy Noble",7)
	SpeccyPrint3( 66, 2,"for `eBlitz Basic `g@ `bAcid `gSoftware",7)

	SpeccyPrint3( 44, 3,"Based on an original game by Matthew Smith",7)

	SpeccyPrint3( 38, 5,"@ 1983 BUG-BYTE Ltd and Software Projects Ltd",7)
	SpeccyPrint3( 78, 6,"@ 1997 Alchemist Research",7)

	SpeccyPrint3( 34, 8,"`dProgramming`e, `dGraphics`e................`fAndy Noble",7)
	SpeccyPrint3( 34, 9,"`dMusic Arranged by`e.................`fMatt Simmonds",7)
	SpeccyPrint3( 34,10,"`dOriginal PC help`e...........`fTyrone L. Cartwright",7)

	SpeccyPrint3( 14,12,"I would just like to say ThankYou to the following people",3)
	SpeccyPrint3( 14,14,"`fJon Dow`e...................`dFor all the mail and the laughs",7)
	SpeccyPrint3( 14,15,"`fJane Stroud`e.........`dFor being there when I needed it most",7)
	SpeccyPrint3( 14,16,"`fDavid Tolley`e...............`dFor year after year after year",7)
	SpeccyPrint3( 14,17,"`fDerek Ham`e........................`dFor illness and worrying",7)
	SpeccyPrint3( 14,18,"`fMark Sibly`e......................................`dFor Blitz",7)
	
	SpeccyPrint3(106,20,"A Hello to:",6)
	SpeccyPrint3( 78,21,"All the guys at Retro`bS`fp`de`ac",7)
	SpeccyPrint3( 62,22,"All the guys on `fCOMP.SYS.SINCLAIR",7)
	SpeccyPrint3( 64,23,"and all the guys at the `eBasement",7)

	SpeccyPrint3(124,26,"F2",6)
	SpeccyPrint3(108,27,"Game Speed",4)

	SpeccyPrint3(  4,31,"Press `eESC`g to quit to Title, `eF10`g at any time to quit to Windows",7)

End Function
;///////////////////////////////////////
;//	Update Prefs
;///////////////////////////////////////
Function	UpdatePrefs()
	WaitVR()
	SwapPage()

	Select PREFSs
		Case	1
			SpeccyPrint3(110,28," BONKERS ",2)
			PREFSs=1
		Case	2
			SpeccyPrint3(110,28,"  SILLY  ",3)
			PREFSs=2
		Case	3
			SpeccyPrint3(112,28,"  FAST  ",6)
			PREFSs=3
		Case	4
			SpeccyPrint3(112,28,"ORIGINAL",7)
			PREFSs=4
		Case	5
			SpeccyPrint3(112,28," SPECCY ",5)
			PREFSs=5
		Case	6
			SpeccyPrint3(112,28," BORING ",1)
			PREFSs=6
	End Select

	If	KeyDown(60)
		If PREFSh=0
			PREFSs=(PREFSs+1) Mod 7
			If PREFSs=0
				PREFSs=1
			End If
			PREFSh=1
		End If
	Else
		PREFSh=0
	End If	

	If	KeyDown(1)
		TITLEm=0
		SPEED=PREFSs
		StopChannel MMusic
		GOS=0
	End If	

	tim2=tim1
End Function