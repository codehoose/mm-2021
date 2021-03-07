;///////////////////////////////////////
;//	Set Up Data
;///////////////////////////////////////
Function	SetupData()

;	Setup Level Data
	Restore	LEVELS
	For	lev=1 To 21
		For	y=1 To 16
			For	x=1 To 32
				Read dat
				ROOMS(lev,y,x)=dat
			Next
		Next
	Next

;	Setup Level Name
	Restore	LEVnames
	For	lev=1 To 21
		Read tex$
		TITLES$(lev)=tex$
	Next

;	Setup Borders
	Restore	LEVbord
	For	lev=1 To 21
		Read dat
		BORDERS(lev)=dat
	Next

;	Setup Willy Start X Position
	Restore	WILLYstartx
	For	lev=1 To 21
		Read dat
		WILLYsx(lev)=dat
	Next
		
;	Setup Willy Start Y Position
	Restore	WILLYstarty
	For	lev=1 To 21
		Read dat
		WILLYsy(lev)=dat
	Next

;	Setup Willy Start Direction
	Restore	WILLYstartd
	For	lev=1 To 21
		Read dat
		WILLYsd(lev)=dat
	Next

;	Setup Conv X Position
	Restore	CONVxpos
	For	lev=1 To 21
		Read dat
		CONVx(lev)=dat
	Next

;	Setup Conv Y Position
	Restore	CONVypos
	For	lev=1 To 21
		Read dat
		CONVy(lev)=dat
	Next

;	Setup Conv Direction
	Restore	CONVdir
	For	lev=1 To 21
		Read dat
		CONVd(lev)=dat
	Next
	
;	Setup Conv Length
	Restore	CONVlen
	For	lev=1 To 21
		Read dat
		CONVl(lev)=dat
	Next

;	Setup Key X Position
	Restore	KEYxpos
	For	lev=1 To 21
		For x=1 To 5
			Read dat
			KEYx(lev,x)=dat
		Next
	Next

;	Setup Key Y Position
	Restore	KEYypos
	For	lev=1 To 21
		For x=1 To 5
			Read dat
			KEYy(lev,x)=dat
		Next
	Next

;	Setup Key Status
	Restore	KEYstat
	For	lev=1 To 21
		For x=1 To 5
			Read dat
			KEYs(lev,x)=dat
		Next
	Next

;	Setup Switch X Position
	Restore	SWITCHxpos
	For	lev=1 To 21
		For x=1 To 2
			Read dat
			SWITCHx(lev,x)=dat
		Next
	Next

;	Setup Switch Y Position
	Restore	SWITCHypos
	For	lev=1 To 21
		For x=1 To 2
			Read dat
			SWITCHy(lev,x)=dat
		Next
	Next

;	Setup Switch X Position
	Restore	SWITCHstat
	For	lev=1 To 21
		For x=1 To 2
			Read dat
			SWITCHs(lev,x)=dat
		Next
	Next

;	Setup Exit X Position
	Restore	EXITxpos
	For	lev=1 To 21
		Read dat
		EXITx(lev)=dat
	Next

;	Setup Exit Y Position
	Restore	EXITypos
	For	lev=1 To 21
		Read dat
		EXITy(lev)=dat
	Next

;	Setup Exit Y Position
	Restore	AIRcount
	For	lev=1 To 21
		Read dat
		AIR(lev)=dat
	Next

;	Setup HRobo X Position
	Restore	HROBOxpos
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			HROBOx(lev,x)=dat
		Next
	Next

;	Setup HRobo Y Position
	Restore	HROBOypos
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			HROBOy(lev,x)=dat
		Next
	Next

;	Setup HRobo MIN Position
	Restore	HROBOminpos
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			HROBOmin(lev,x)=dat
		Next
	Next

;	Setup HRobo MAX Position
	Restore	HROBOmaxpos
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			HROBOmax(lev,x)=dat
		Next
	Next

;	Setup HRobo Direction 
	Restore	HROBOdir
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			HROBOd(lev,x)=dat
		Next
	Next

;	Setup HRobo Speed 
	Restore	HROBOspeed
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			HROBOs(lev,x)=dat
		Next
	Next

;	Setup HRobo Graphic 
	Restore	HROBOgra
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			HROBOgfx(lev,x)=dat
		Next
	Next

;	Setup HRobo Flip 
	Restore	HROBOfli
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			HROBOflip(lev,x)=dat
		Next
	Next

;	Setup HRobo Anim
	Restore	HROBOani
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			HROBOanim(lev,x)=dat
		Next
	Next

;	Setup VRobo X Position
	Restore	VROBOxpos
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			VROBOx(lev,x)=dat
		Next
	Next

;	Setup VRobo Y Position
	Restore	VROBOypos
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			VROBOy(lev,x)=dat
		Next
	Next

;	Setup VRobo MIN Position
	Restore	VROBOminpos
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			VROBOmin(lev,x)=dat
		Next
	Next

;	Setup VRobo MAX Position
	Restore	VROBOmaxpos
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			VROBOmax(lev,x)=dat
		Next
	Next

;	Setup VRobo Direction 
	Restore	VROBOdir
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			VROBOd(lev,x)=dat
		Next
	Next

;	Setup VRobo Speed
	Restore	VROBOspeed
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			VROBOs(lev,x)=dat
		Next
	Next


;	Setup VRobo Graphic 
	Restore	VROBOgra
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			VROBOgfx(lev,x)=dat
		Next
	Next

;	Setup VRobo Anim
	Restore	VROBOani
	For	lev=1 To 21
		For x=1 To 4
			Read dat
			VROBOanim(lev,x)=dat
		Next
	Next

;	Setup SKY
	Restore	SKYxpos
	For	lev=1 To 3
		For x=1 To 4
			Read dat
			SKYpx(lev,x)=dat
		Next
	Next

;	Setup SKY
	Restore	SKYypos
	For	lev=1 To 3
		For x=1 To 4
			Read dat
			SKYpy(lev,x)=dat
		Next
	Next

;	Setup CHEAT
	Restore	CHEATcode
	For	lev=1 To 6
		Read dat
		CHEATkey(lev)=dat
	Next

End Function