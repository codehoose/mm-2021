;///////////////////////////////////////
;//	Do Willy
;///////////////////////////////////////
Function	DoWilly()

	INKEY=GetWillyInput()

	CheckRoboHit()

	If CheckWillyKillBlock()<>0
		cWILLYm=6
	End If

	If cWILLYm=0
		CheckWillyFall()
		CheckWillyConv()
	End If

	Select cWILLYm
		Case	0
			CheckCrumb()
;---------------------------------------
			If INKEY=1
				DoWillyLeft()
				cWILLYfall=0
			Else
;---------------------------------------
				If INKEY=2
					DoWillyRight()
					cWILLYfall=0
				Else
;---------------------------------------
					If INKEY=4
						cWILLYm=1
						cWILLYj=0
						cWILLYjs=0
						cWILLYfall=0
						DoWillyJump()
					Else
;---------------------------------------
						If INKEY=5
							If cWILLYd=0
								cWILLYd=1
								cWILLYm=1
								cWILLYj=0
								cWILLYjs=0
								cWILLYfall=0
								DoWillyJump()
							Else
								cWILLYm=2
								cWILLYj=0
								cWILLYjs=0
								cWILLYfall=0
								DoWillyLeft()
								DoWillyJump()
							End If
						Else
;---------------------------------------
							If INKEY=6
								If cWILLYd=1
									cWILLYd=0
									cWILLYm=1
									cWILLYj=0
									cWILLYjs=0
									cWILLYfall=0
									DoWillyJump()
								Else
									cWILLYm=3
									cWILLYj=0
									cWILLYjs=0
									cWILLYfall=0
									DoWillyRight()
									DoWillyJump()
								End If
;---------------------------------------
							Else
									cWILLYjs=0
									cWILLYfall=0
							End If
						End If
					End If
				End If
			End If
		Case	1
			DoWillyJump()
		Case	2
			DoWillyLeft()
			DoWillyJump()
		Case	3
			DoWillyRight()
			DoWillyJump()
		Case	4
			DoWillyFall()
		Case	6
			DoDeath()
	End Select
	CheckKeys()
	CheckExit()
	CheckSwitches()
	DrawWilly()
End Function
;///////////////////////////////////////
;//	Get Willys Input
;///////////////////////////////////////
Function	GetWillyInput()
	lft=0
	rgt=0
	jmp=0
	
	If KeyDown(16) Or KeyDown(18) Or KeyDown(20) Or KeyDown(22) Or KeyDown(24) Or KeyDown(26) Or KeyDown(203)
		lft=1
	End If
	
	If KeyDown(17) Or KeyDown(19) Or KeyDown(21) Or KeyDown(23) Or KeyDown(25) Or KeyDown(27) Or KeyDown(205)
		rgt=2
	End If
	
	If KeyDown(42) Or KeyDown(43) Or KeyDown(44) Or KeyDown(45) Or KeyDown(46) Or KeyDown(47) Or KeyDown(48) Or KeyDown(49) Or KeyDown(50) Or KeyDown(51) Or KeyDown(52) Or KeyDown(53) Or KeyDown(54) Or KeyDown(57) Or KeyDown(200)
		jmp=4
	End If

	Return(lft Or rgt Or jmp)
End Function
;///////////////////////////////////////
;//	Get a Block
;///////////////////////////////////////
Function	GetBlock(x,y)
	Return(cROOM((y/8)+1,(x/8)+1))
End Function
;///////////////////////////////////////
;//	Check Crumbling Block
;///////////////////////////////////////
Function	CheckCrumb()
	blk1=cCRUMB((cWILLYy/8)+3,(cWILLYx/8)+1)
	blk2=cCRUMB((cWILLYy/8)+3,(cWILLYx/8)+2)

	If blk1<>0
		blk1=blk1-1
		cCRUMB((cWILLYy/8)+3,(cWILLYx/8)+1)=blk1
		If blk1=0
			cROOM((cWILLYy/8)+3,(cWILLYx/8)+1)=0
		End If
	End If

	If blk2<>0
		blk2=blk2-1
		cCRUMB((cWILLYy/8)+3,(cWILLYx/8)+2)=blk2
		If blk2=0
			cROOM((cWILLYy/8)+3,(cWILLYx/8)+2)=0
		End If
	End If
End Function
;///////////////////////////////////////
;//	Draw Willy
;///////////////////////////////////////
Function	DrawWilly()
	If cWILLYd
		DrawImage(image16,cWILLYx And 248,cWILLYy,8+((cWILLYx And 15)Shr 1))
	Else
		DrawImage(image16,cWILLYx And 248,cWILLYy,(cWILLYx And 15)Shr 1)
	End If

End Function
;///////////////////////////////////////
;//	Do Willy Left
;///////////////////////////////////////
Function	DoWillyLeft()
	If cWILLYd=0
		cWILLYd=1
	Else
		cWILLYx=cWILLYx-2
		blk1=GetBlock(cWILLYx,cWILLYy)
		blk2=GetBlock(cWILLYx,cWILLYy+8)
		blk3=GetBlock(cWILLYx,cWILLYy+12)
		If blk1=3 Or blk2=3 Or blk3=3
			cWILLYx=cWILLYx+2
		End If
	End If
End Function
;///////////////////////////////////////
;//	Do Willy Right
;///////////////////////////////////////
Function	DoWillyRight()
	If cWILLYd=1
		cWILLYd=0
	Else
		cWILLYx=cWILLYx+2
		blk1=GetBlock(cWILLYx+8,cWILLYy)
		blk2=GetBlock(cWILLYx+8,cWILLYy+8)
		blk3=GetBlock(cWILLYx+8,cWILLYy+12)
		If blk1=3 Or blk2=3 Or blk3=3
			cWILLYx=cWILLYx-2
		End If
	End If
End Function
;///////////////////////////////////////
;//	Do Willy Jump
;///////////////////////////////////////
Function	DoWillyJump()
	jp=((cWILLYj And 254)-8)/2
	cWILLYy=(cWILLYy+jp)

	If cWILLYj<8
		blk1=GetBlock(cWILLYx,cWILLYy)
		blk2=GetBlock(cWILLYx+8,cWILLYy)
		If blk1=3 Or blk2=3
			cWILLYm=4
			cWILLYjs=0
			cWILLYy=(cWILLYy+8) And 248
		End If
	End If

	If cWILLYj>11
		If (cWILLYy And 7)=0
			blk1=GetBlock(cWILLYx,cWILLYy+16)
			blk2=GetBlock(cWILLYx+8,cWILLYy+16)
			If blk1<>0 Or blk2<>0
				cWILLYm=0
				cWILLYj=0
				cWILLYy=(cWILLYy And 248)
			End If
		End If
	End If
	
	cWILLYj=cWILLYj+1
	If cWILLYj=18
		cWILLYm=0
		cWILLYj=0
		CheckWillyFall()
	End If

	If cWILLYj<11
		cWILLYjs=cWILLYjs+1
	Else
		If cWILLYj>10
			cWILLYjs=cWILLYjs-1
		End If
	End If

	If cWILLYj>12
		cWILLYfall=(cWILLYfall+jp)
	End If

	If cWILLYm<>0
		SoundPitch SFXjump,16384+(cWILLYjs*1500)
		PlaySound SFXjump
	End If
End Function
;///////////////////////////////////////
;//	Check Willy Fall
;///////////////////////////////////////
Function	CheckWillyFall()
	blk1=GetBlock(cWILLYx,cWILLYy+16)
	blk2=GetBlock(cWILLYx+8,cWILLYy+16)
	If blk1=0 And blk2=0
		cWILLYm=4
		cWILLYjs=0
	End If
End Function
;///////////////////////////////////////
;//	Do Willy Fall
;///////////////////////////////////////
Function	DoWillyFall()
	cWILLYy=cWILLYy+4
	blk1=GetBlock(cWILLYx,cWILLYy+16)
	blk2=GetBlock(cWILLYx+8,cWILLYy+16)
	If blk1<>0 Or blk2<>0
		cWILLYy=(cWILLYy And 248)
		cWILLYm=0
		If cWILLYfall>=32
			cWILLYm=6
		Else
			cWILLYfall=0
		End If
	Else
		cWILLYfall=cWILLYfall+4
	End If
	cWILLYjs=(cWILLYjs+1) Mod 11
	SoundPitch SFXjump,16384-(cWILLYjs*1000)
	PlaySound SFXjump
End Function
;///////////////////////////////////////
;//	Check Willy hit Kill block
;///////////////////////////////////////
Function	CheckWillyKillBlock()
	blk1=GetBlock(cWILLYx,cWILLYy)
	blk2=GetBlock(cWILLYx+8,cWILLYy)
	blk3=GetBlock(cWILLYx,cWILLYy+8)
	blk4=GetBlock(cWILLYx+8,cWILLYy+8)
	blk5=GetBlock(cWILLYx,cWILLYy+16)
	blk6=GetBlock(cWILLYx+8,cWILLYy+16)

	hit=0

	If blk1=5 Or blk2=5 Or blk3=5 Or blk4=5 Or blk5=5 Or blk6=5
		hit=1
	End If
	
	If blk1=6 Or blk2=6 Or blk3=6 Or blk4=6 Or blk5=6 Or blk6=6
		hit=1
	End If
	Return(hit)
End Function
;///////////////////////////////////////
;//	Do Willy Death
;///////////////////////////////////////
Function	DoDeath()
	GAMEmode=2
	DEATHm=0
	DEATHc=0
	DEATHi=CopyImage(imageGAME)
	If ChannelPlaying(MMusic)
		StopChannel MMusic
	End If
	PlaySound SFXdie
End Function
;///////////////////////////////////////
;//	Check Keys
;///////////////////////////////////////
Function	CheckKeys()
	For i=1 To 5
		If cKEYSs(i)=1
			If RectsOverlap(cKEYSx(i),cKEYSy(i),8,8,cWILLYx,cWILLYy,10,18)=1
				cKEYSs(i)=0
				PlaySound SFXpick
				SCORE=SCORE+100
			End If
		End If
	Next
End Function
;///////////////////////////////////////
;//	Check Exit
;///////////////////////////////////////
Function	CheckExit()
	If cEXITs=1
		If RectsOverlap(cEXITx,cEXITy,16,16,cWILLYx+4,cWILLYy+8,2,2)=1
			If ROOM=20 And CHEAT=0
				GAMEmode=6
				LASTm=0
			Else
				GAMEmode=3
			End If
		End If
	End If
End Function
;///////////////////////////////////////
;//	Check is Willy On Conv
;///////////////////////////////////////
Function	CheckWillyConv()
	If WillyOnConv()=1
		If cWILLYd<>cCONVd Or (INKEY And 3)=0
			If cCONVd=0
				INKEY=((INKEY And 253) Or 1)
			Else
				INKEY=((INKEY And 254) Or 2)
			End If
		End If
	End If
End Function
;///////////////////////////////////////
;//	Check is Willy On Conv
;///////////////////////////////////////
Function	WillyOnConv()
	blk1=GetBlock(cWILLYx,cWILLYy+16)
	blk2=GetBlock(cWILLYx+8,cWILLYy+16)
	If blk1=7 Or blk2=7
		Return 1
	Else
		Return 0
	End If
End Function
;///////////////////////////////////////
;//	Check if hit Robo
;///////////////////////////////////////
Function	CheckRoboHit()
	For i=1 To 4
		If cHROBOx(i)<>-1
			If cHROBOd(i)=0
				If cWILLYd
					If ImagesCollide(image16,cWILLYx And 248,cWILLYy,8+((cWILLYx And 15)Shr 1),image16,cHROBOx(i) And 248,cHROBOy(i),cHROBOgfx(i)+((cHROBOx(i) And cHROBOanim(i))/2))=1
						cWILLYm=6
					End If
				Else
					If ImagesCollide(image16,cWILLYx And 248,cWILLYy,(cWILLYx And 15)Shr 1,image16,cHROBOx(i) And 248,cHROBOy(i),cHROBOgfx(i)+((cHROBOx(i) And cHROBOanim(i))/2))=1
						cWILLYm=6
					End If
				End If
			Else
				If cWILLYd
					If ImagesCollide(image16,cWILLYx And 248,cWILLYy,8+((cWILLYx And 15)Shr 1),image16,cHROBOx(i) And 248,cHROBOy(i),(cHROBOgfx(i)+((cHROBOx(i) And cHROBOanim(i))/2))+cHROBOflip(i))=1
						cWILLYm=6
					End If
				Else
					If ImagesCollide(image16,cWILLYx And 248,cWILLYy,(cWILLYx And 15)Shr 1,image16,cHROBOx(i) And 248,cHROBOy(i),(cHROBOgfx(i)+((cHROBOx(i) And cHROBOanim(i))/2))+cHROBOflip(i))=1
						cWILLYm=6
					End If
				End If
			End If			
		End If
	Next
	
	For i=1 To 4
		If cVROBOx(i)<>-1
			If cWILLYd
				If ImagesCollide(image16,cWILLYx And 248,cWILLYy,8+((cWILLYx And 15)Shr 1),image16,cVROBOx(i),cVROBOy(i),cVROBOgfx(i)+cVROBOanim(i))=1
					cWILLYm=6
				End If
			Else
				If ImagesCollide(image16,cWILLYx And 248,cWILLYy,(cWILLYx And 15)Shr 1,image16,cVROBOx(i),cVROBOy(i),cVROBOgfx(i)+cVROBOanim(i))=1
					cWILLYm=6
				End If
			End If
		End If
	Next

	Select ROOM
		Case 5
			If cWILLYd
				If ImagesCollide(image16,cWILLYx And 248,cWILLYy,8+((cWILLYx And 15)Shr 1),image16,EUGENEx,EUGENEy,418)=1
					cWILLYm=6
				End If
			Else
				If ImagesCollide(image16,cWILLYx And 248,cWILLYy,(cWILLYx And 15)Shr 1,image16,EUGENEx,EUGENEy,418)=1
					cWILLYm=6
				End If
			End If
		Case	8
			If KONGm=0
				If cWILLYd
					If ImagesCollide(image16,cWILLYx And 248,cWILLYy,8+((cWILLYx And 15)Shr 1),image16,KONGx,KONGy,408+KONGf)=1
						cWILLYm=6
					End If
				Else
					If ImagesCollide(image16,cWILLYx And 248,cWILLYy,(cWILLYx And 15)Shr 1,image16,KONGx,KONGy,408+KONGf)=1
						cWILLYm=6
					End If
				End If
			Else
				If KONGm=1
					If cWILLYd
						If ImagesCollide(image16,cWILLYx And 248,cWILLYy,8+((cWILLYx And 15)Shr 1),image16,KONGx,KONGy,410+KONGf)=1
							cWILLYm=6
						End If
					Else
						If ImagesCollide(image16,cWILLYx And 248,cWILLYy,(cWILLYx And 15)Shr 1,image16,KONGx,KONGy,410+KONGf)=1
							cWILLYm=6
						End If
					End If
				End If
			End If
		Case	12
			If KONGm=0
				If cWILLYd
					If ImagesCollide(image16,cWILLYx And 248,cWILLYy,8+((cWILLYx And 15)Shr 1),image16,KONGx,KONGy,408+KONGf)=1
						cWILLYm=6
					End If
				Else
					If ImagesCollide(image16,cWILLYx And 248,cWILLYy,(cWILLYx And 15)Shr 1,image16,KONGx,KONGy,408+KONGf)=1
						cWILLYm=6
					End If
				End If
			Else
				If KONGm=1
					If cWILLYd
						If ImagesCollide(image16,cWILLYx And 248,cWILLYy,8+((cWILLYx And 15)Shr 1),image16,KONGx,KONGy,410+KONGf)=1
							cWILLYm=6
						End If
					Else
						If ImagesCollide(image16,cWILLYx And 248,cWILLYy,(cWILLYx And 15)Shr 1,image16,KONGx,KONGy,410+KONGf)=1
							cWILLYm=6
						End If
					End If
				End If
			End If
		Case	14
			For i=1 To 3
				If cWILLYd
					If ImagesCollide(image16,cWILLYx And 248,cWILLYy,8+((cWILLYx And 15)Shr 1),image16,SKYx(i),SKYy(i),(252+(i*8))+SKYf(i))=1
						cWILLYm=6
					End If
				Else
					If ImagesCollide(image16,cWILLYx And 248,cWILLYy,(cWILLYx And 15)Shr 1,image16,SKYx(i),SKYy(i),(252+(i*8))+SKYf(i))=1
						cWILLYm=6
					End If
				End If
			Next
	End Select
End Function
;///////////////////////////////////////
;//	Check Switches
;///////////////////////////////////////
Function	CheckSwitches()
	For i=1 To 2
		If cSWITCHs(i)=1
			If RectsOverlap(cSWITCHx(i),cSWITCHy(i),8,8,cWILLYx,cWILLYy,10,18)=1
				cSWITCHs(i)=2
			End If
		End If
	Next
End Function
;///////////////////////////////////////
;//	Check SPG
;///////////////////////////////////////
Function	CheckSPG()
	For i=0 To 64
		If SPGx(i)<>-1
			If RectsOverlap(SPGx(i)*8,SPGy(i)*8,8,8,cWILLYx,cWILLYy,8,16)=1
				cAIRp=cAIRp/2
			End If
		End If
	Next
End Function