;///////////////////////////////////////
;//
;//	Mainc Miner for Blitz Basic
;//
;///////////////////////////////////////
AppTitle "Manic Miner for Blitz Basic"
Graphics 320,240,16,0
LoadHiScore()
;///////////////////////////////////////
;//	Load Graphics
;///////////////////////////////////////
Global 	image16=LoadAnimImage("data\gfx\16x16.bmp",16,16,0,484)
Global	imageBG=LoadImage("data\gfx\background.bmp")
Global	imageBGf1=LoadImage("data\gfx\bgfill1.bmp")
Global	imageBGf2=LoadImage("data\gfx\bgfill2.bmp")
Global 	imageBLOCKS=LoadAnimImage("data\gfx\blocks.bmp",8,8,0,320)
Global	imageEND=LoadImage("data\gfx\end.bmp")
Global	imagePIANO=LoadImage("data\gfx\piano.bmp")
Global	imageSUN=LoadImage("data\gfx\sun.bmp")
Global	imageSWITCH=LoadAnimImage("data\gfx\switches.bmp",8,8,0,2)
Global	imageTITLE=LoadAnimImage("data\gfx\titleair.bmp",256,8,0,2)
Global	imageFONT=LoadAnimImage("data\gfx\font.bmp",8,8,0,768)
Global	imageSMALL=LoadAnimImage("data\gfx\fonts.bmp",4,6,0,512)
Global	imageLOGO=LoadImage("data\gfx\logo.bmp")

Global	imageGAME=CreateImage(256,192)	;Actual Game Screen
Global	imageROOM=CreateImage(256,128)	;Screen where Room is built up
Global	imageCRUMB=CreateImage(8,8)		;Buffer where Crumbling Blocks are drawn to.

;///////////////////////////////////////
;//	Load Sound
;///////////////////////////////////////
Global	SFXjump=LoadSound("data\sfx\jump.wav")
Global	SFXjump2=LoadSound("data\sfx\jump.wav")
Global	SFXdie=LoadSound("data\sfx\die.wav")
Global	SFXpick=LoadSound("data\sfx\pick.wav")
;///////////////////////////////////////
;//	Variables
;///////////////////////////////////////
Global	Version$="V0.82"
Global	QUIT

Global	ROOM%

Global	INKEY%

Global	MODE%
Global	OLDMODE%
Global	SUREm%
Global	SUREs%
Global	SUREi%
Global	SURE2m%
Global	SURE2s%
Global	SURE2i%
Global	TITLEm%
Global	TITLEc%
Global	TITLEi%

Global	DEMOm%
Global	DEMOp%
Global	DEMOi%

Global	MMusic%
Global	MUSICon%
Global	MUSICh%

Global	SCROLLpos%
Global	SCROLLpixel%
Global	SCROLLwf%
Global	SCROLLwp%

Global	LOADm%
Global	LOADc%

Global	GAMEmode%

Global	LIVES%
Global	LIVESp%
Global	LIVESf%

Global	EUGENEx%
Global	EUGENEy%
Global	EUGENEd%
Global	EUGENEm%
Global	EUGENEc%
Global	EUGENEmin%
Global	EUGENEmax%

Global	KONGm%
Global	KONGx%
Global	KONGy%
Global	KONGp%
Global	KONGf%

Global	HOLEl%
Global	HOLEy%

Dim	SKYx%(3)
Dim	SKYy%(3)
Dim	SKYm%(3)
Dim	SKYf%(3)
Dim	SKYp%(3)
Dim	SKYs%(3)
Dim	SKYmax%(3)

Dim	SPGx%(64)
Dim	SPGy%(64)

Global	DEATHm%
Global	DEATHc%

Global	SPEED%
Global	OLDSPEED%

Global	OVERm%
Global	OVERi%
Global	OVERp%
Global	BOOTy%

Global	SCORE%
Global	HISCORE%
Global	EXTRA%
Global	EXTRAm%
Global	EXTRAx%
Global	EXTRAdelta%

Global	FILE%
Global	LOGOy#
Global	LOGOthrust#
Global	LOGOacc#

Global	PAUSE%
Global	PAUSEh%
Global	PAUSEi%
Global	PAUSEx%
Global	PAUSEy%
Global	PAUSEr%
Global	PAUSEim%

Global	PREFSm%
Global	PREFSs%
Global	PREFSh%

Global	CHEAT%
Dim		CHEATkey%(6)
Global	CHEATp%
Global	CHEATh%

Global	GOS%

Global	LASTm%
Global	LASTf%
Global	LASTp%

Global	TEXTm%
Global	TEXTp%
Global	TEXTid%
Global	TEXTik%

Include "mm-end.bb"

;///////////////////////////////////////
;//	Level Variables
;///////////////////////////////////////
Dim	ROOMS%(21,16,32)
Dim	TITLES$(21)
Dim	BORDERS%(21)
Dim	WILLYsx%(21)
Dim	WILLYsy%(21)
Dim	WILLYsd%(21)
Dim	CONVx%(21)
Dim	CONVy%(21)
Dim	CONVd%(21)
Dim	CONVl%(21)
Dim	KEYx%(21,5)
Dim	KEYy%(21,5)
Dim	KEYs%(21,5)
Dim	SWITCHx%(21,2)
Dim	SWITCHy%(21,2)
Dim	SWITCHs%(21,2)
Dim	EXITx%(21)
Dim	EXITy%(21)
Dim	AIR%(21)
Dim	HROBOx%(21,4)
Dim	HROBOy%(21,4)
Dim	HROBOmin%(21,4)
Dim	HROBOmax%(21,4)
Dim	HROBOd%(21,4)
Dim	HROBOs%(21,4)
Dim	HROBOgfx%(21,4)
Dim	HROBOflip%(21,4)
Dim	HROBOanim%(21,4)
Dim	VROBOx%(21,4)
Dim	VROBOy%(21,4)
Dim	VROBOmin%(21,4)
Dim	VROBOmax%(21,4)
Dim	VROBOd%(21,4)
Dim	VROBOs%(21,4)
Dim	VROBOgfx%(21,4)
Dim	VROBOanim%(21,4)
Dim	SKYpx%(3,4)
Dim	SKYpy%(3,4)
;///////////////////////////////////////
;//	Current Room
;///////////////////////////////////////
Global	BLOCKoff%
Dim	cROOM%(16,32)
Dim	cCRUMB%(16,32)
Global	cTITLE$
Global	cBORDER%
Global	cAIR%
Global	cAIRp%
Dim	cKEYSx%(5)
Dim	cKEYSy%(5)
Dim	cKEYSs%(5)
Dim	cKEYSb%(5)
Dim	cKEYSbp%(5)
Dim	cSWITCHx%(2)
Dim	cSWITCHy%(2)
Dim	cSWITCHs%(2)
Global	cSWITCH1m%
Global	cSWITCH2m%
Global	cCONVx%
Global	cCONVy%
Global	cCONVd%
Global	cCONVl%
Global	cCONVanim%
Global	cEXITx%
Global	cEXITy%
Global	cEXITs%
Global	cEXITanim%
Global	cEXITi%
Dim	cHROBOx%(4)
Dim	cHROBOy%(4)
Dim	cHROBOmin%(4)
Dim	cHROBOmax%(4)
Dim	cHROBOd%(4)
Dim	cHROBOs%(4)
Dim	cHROBOgfx%(4)
Dim	cHROBOflip%(4)
Dim	cHROBOanim%(4)
Dim	cVROBOx%(4)
Dim	cVROBOy%(4)
Dim	cVROBOmin%(4)
Dim	cVROBOmax%(4)
Dim	cVROBOd%(4)
Dim	cVROBOs%(4)
Dim	cVROBOgfx%(4)
Dim	cVROBOanim%(4)

Global	cWILLYm%
Global	cWILLYx%
Global	cWILLYy%
Global	cWILLYd%
Global	cWILLYj%
Global	cWILLYjs%
Global	cWILLYfall%

Global	tim1%
Global	tim2%
;///////////////////////////////////////
;//	Setup Data
;///////////////////////////////////////
SetupData()
;///////////////////////////////////////
;//	Set Initial State
;///////////////////////////////////////
QUIT=0
MODE=3
LOADm=0

CHEAT=0
CHEATh=0
CHEATp=1
CHEATh=0

GOS=0
;///////////////////////////////////////
;//	Main Code
;///////////////////////////////////////
tim1=MilliSecs()
Repeat
	Select MODE
		Case	0
			MainTitles()
		Case	1
			DoGame()
		Case	2
			DoDemo()
		Case	3
			Load()
		Case	5
			SwapPage()
			AreYouSure2()
	End Select
	If KeyDown(68) And MODE<>5 And MODE<>3
		OLDMODE=MODE
		SURE2m=0
		SURE2s=0
		MODE=5
	End If	

	If KeyDown(87)
		SaveScreenshot()
	End If

Until QUIT<>0

StopChannel MMusic
SaveHiScore()

End
;///////////////////////////////////////
;//	Wait My VR
;///////////////////////////////////////
Function	WaitVR()
	While ((tim1-tim2)<(SPEED*12))
		tim1=MilliSecs()
	Wend
End Function
;///////////////////////////////////////
;//	Swap Page
;///////////////////////////////////////
Function	SwapPage()
	SetBuffer(BackBuffer())
	DrawBlock(imageGAME,32,24)
	Flip
	BufferGame()
End Function
;///////////////////////////////////////
;//	Buffer Game
;///////////////////////////////////////
Function	BufferGame()
	SetBuffer(ImageBuffer(imageGAME))
End Function
;///////////////////////////////////////
;//	Buffer Room
;///////////////////////////////////////
Function	BufferRoom()
	SetBuffer(ImageBuffer(imageROOM))
End Function
;///////////////////////////////////////
;//	Buffer Crumbling Blocks
;///////////////////////////////////////
Function	BufferCrumb()
	SetBuffer(ImageBuffer(imageCRUMB))
End Function
;///////////////////////////////////////
;//	Speccy Print
;///////////////////////////////////////
Function SpeccyPrint(x,y,ss$,col)
	xpos=0
	For i=0 To Len(ss$)-1
		cr=(Asc(Mid$(ss$,i+1,1))-32)
		If cr=(Asc("`")-32)
			col=(Asc(Mid$(ss$,i+2,1))-96)
			i=i+1
		Else
			DrawImage(imageFont,x+(xpos*8),y,cr+(col*96))
			xpos=xpos+1
		End If
	Next
End Function
;///////////////////////////////////////
;//	Speccy Print 2 (Overwrite)
;///////////////////////////////////////
Function SpeccyPrint2(x,y,ss$,col)
	xpos=0
	For i=0 To Len(ss$)-1
		cr=(Asc(Mid$(ss$,i+1,1))-32)
		If cr=(Asc("`")-32)
			col=(Asc(Mid$(ss$,i+2,1))-96)
			i=i+1
		Else
			DrawBlock(imageFont,x+(xpos*8),y,cr+(col*96))
			xpos=xpos+1
		End If
	Next
End Function
;///////////////////////////////////////
;//	Speccy Print 3 (Small Overwrite)
;///////////////////////////////////////
Function SpeccyPrint3(x,y,ss$,col)
	st$=Upper$(ss$)
	y=y*6
	xpos=0
	For i=0 To Len(st$)-1
		cr=(Asc(Mid$(st$,i+1,1))-32)
		If cr=(Asc("`")-32)
			col=(Asc(Mid$(st$,i+2,1))-64)
			i=i+1
		Else
			If cr>0
				DrawBlock(imageSMALL,x+(xpos*4),y,(cr-1)+(col*64))
			Else
				DrawBlock(imageSMALL,x+(xpos*4),y,0)
			End If
			xpos=xpos+1
		End If
	Next
End Function
;///////////////////////////////////////
;//	Any Key
;///////////////////////////////////////
Function	AnyKey()
	hit=0
	For i=0 To 255
		If KeyDown(i)
			hit=1
		End If
	Next
	Return hit
End Function
;///////////////////////////////////////
;//	Border
;///////////////////////////////////////
Function Border(b)
	SetBuffer(BackBuffer())
	Select	b
		Case	0
			Color	0,0,0
		Case	1
			Color	0,0,163
		Case	2
			Color	223,0,0
		Case	3
			Color	231,0,183
		Case	4
			Color	0,215,0
		Case	5
			Color	0,215,215
		Case	6
			Color	211,211,0
		Case	7
			Color	203,203,203
	End Select

	Rect 	0,0,320,24,True
	Rect 	0,216,320,24,True
	Rect 	0,24,32,192,True
	Rect 	288,24,32,192,True
	BufferGame()
End Function	
;///////////////////////////////////////
;//	Save HiScore
;///////////////////////////////////////
Function	SaveHiScore()
	FILE=WriteFile("data\hiscore.dat")
	WriteInt FILE,HISCORE
	WriteInt FILE,MUSICon
	WriteInt FILE,SPEED
	CloseFile FILE
End Function
;///////////////////////////////////////
;//	Load HiScore
;///////////////////////////////////////
Function	LoadHiScore()
	If FileType("data\hiscore.dat")<>1
		HISCORE=0
		MUSICon=1
		SPEED=4
	Else
		FILE=ReadFile("data\hiscore.dat")
		HISCORE=ReadInt(FILE)
		MUSICon=ReadInt(FILE)
		SPEED=ReadInt(FILE)
		CloseFile FILE
	End If
End Function
;///////////////////////////////////////
;//	Are You Sure
;///////////////////////////////////////
Function	AreYouSure2()
	INKEY=GetWillyInput()
	Select SURE2m	
		Case 0
			PAUSEim=CopyImage(imageGAME)
			PauseChannel MMusic
			DrawBlock(PAUSEim,0,0)
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
			SURE2s=1
			SpeccyPrint(92,64, "YES",1)
			SpeccyPrint(148,64,"NO",6)
			SURE2m=1
			SURE2i=0
		Case	1
			If INKEY=1
				SURE2s=0
			Else
				If INKEY=2
					SURE2s=1
				Else
					If INKEY=4
						If SURE2s=0
							QUIT=1
							If GOS=1
								SPEED=OLDSPEED
							End If
						Else
							MODE=OLDMODE
							ResumeChannel MMusic
							DrawBlock(PAUSEim,0,0)
							INKEY=0
						End If
					Else
						If SURE2s=0
							SpeccyPrint(92,64, "YES",(SURE2i/4)+1)
							SpeccyPrint(148,64,"NO",1)
						Else
							SpeccyPrint(92,64, "YES",1)
							SpeccyPrint(148,64,"NO",(SURE2i/4)+1)
						End If			
					End If
				End If
			End If
		End Select
		SURE2i=(SURE2i+1)Mod 24
End Function
;///////////////////////////////////////
;//	Save a Screen Shot
;///////////////////////////////////////
Function	SaveScreenshot()
	done=0
	count=0
	Repeat
		If FileType("data\screen"+count+".bmp")=0
			done=1
		Else
			count=count+1
			If count=999
				done=1
			End If
		End If
	Until done<>0
	SaveBuffer(FrontBuffer(),"data\screen"+count+".bmp")
End Function
;///////////////////////////////////////
;//	Code Includes
;///////////////////////////////////////
Include	"mm-map.bb"
Include	"mm-title.bb"
Include	"mm-demo.bb"
Include	"mm-game.bb"
Include "mm-willy.bb"
Include "mm-data.bb"