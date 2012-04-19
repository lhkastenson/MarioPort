using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Dos;

namespace MarioPort
{
   class Keyboard
   {

          const char kb1 = (char)2, kbQ = (char)16, kbA = (char)30, kbZ = (char)44;
          const char kb2 = (char)3, kbW = (char)17, kbS = (char)31, kbX = (char)45;
          const char kb3 = (char)4, kbE = (char)18, kbD = (char)32, kbC = (char)46;
          const char kb4 = (char)5, kbR = (char)19, kbF = (char)33, kbV = (char)47;
          const char kb5 = (char)6, kbT = (char)20, kbG = (char)34, kbB = (char)48;
          const char kb6 = (char)7, kbY = (char)21, kbH = (char)35, kbN = (char)49;
          const char kb7 = (char)8, kbU = (char)22, kbJ = (char)36, kbM = (char)50;
          const char kb8 = (char)9, kbI = (char)23, kbK = (char)37;
          const char kb9 = (char)10, kbO = (char)24, kbL = (char)38;
          const char kb0 = (char)11, kbP = (char)25;

          const char kbEsc        = (char)1;
          const char kbBS         = (char)14;
          const char kbTab        = (char)15;
          const char kbEnter      = (char)28;
          const char kbSP         = (char)57;
          const char kbUpArrow    = (char)72;
          const char kbLeftArrow  = (char)75;
          const char kbRightArrow = (char)77;
          const char kbDownArrow  = (char)80;

          char Key;
          byte bKey; 

          const int MaxKeys = 9;
          const int keyLeft   = 1;
          const int keyRight  = 2;
          const int keyUp     = 3;
          const int keyDown   = 4;
          const int keyAlt    = 5;
          const int keyCtrl   = 6;
          const int keyShiftL = 7;
          const int keyShiftR = 8;
          const int keySpace  = 9;
          const int MAX_SEQ_LEN = 100;

          ushort[] KeySeq = new ushort[MAX_SEQ_LEN - 1]; 

          /*KeySeq*/int[] Sequence = new /*KeySeq*/int[MaxKeys]; 
          ushort[] SeqPos = new ushort[MaxKeys]; 

          bool Recording = false; 
          bool Playing = false; 

          //OldKbIntVec: Procedure;
          //OldExitProc: Pointer;

          bool[] KeyMap = new bool [MaxKeys]; //: Array[1..MaxKeys] of Boolean =
            //(False, False, False, False, False, False, False, False, False);

          char[] PressCode = new char [MaxKeys]; //: Array[1..MaxKeys] of Char =
            //('K', 'M', 'H', 'P', '8', #29, '*', '6', '9');

          char[] ReleaseCode = new char [MaxKeys]; //: Array[1..MaxKeys] of Char =
            //(#203, #205, #200, #208, #184, #157, #170, #182, #185);

          bool HandlerActive = false; //: Boolean = FALSE;
          bool KeyHit = false; //: Boolean = FALSE;

        public void NewExitProc()
        {
           //  KeyBoardDone;
           //  ExitProc = OldExitProc;
        }

        public void GetKey()
        {
           //asm
           //        pushf
           //        push    ax
           //        push    cx
           //        push    dx
           //        push    di
           //        push    es
           //        mov     ax, seg @Data
           //        mov     es, ax
           //        inc     es:KeyHit
           //        in      al, 60h
           //        mov     dl, al
           //        mov     es:Key, al
           //        mov     di, offset PressCode
           //        mov     cx, MaxKeys
           //        cld
           //        repnz
           //        scasb
           //        jnz     @1
           //        mov     di, offset KeyMap[MaxKeys]
           //        sub     di, cx
           //        mov     al, 1
           //        dec     di
           //        stosb
           //        jmp     @2
           //@1:     mov     di, offset ReleaseCode
           //        mov     cx, MaxKeys
           //        cld
           //        repnz
           //        scasb
           //        jnz     @2
           //        mov     es:KeyHit, 0
           //        mov     di, offset KeyMap[MaxKeys]
           //        sub     di, cx
           //        mov     al, 0
           //        dec     di
           //        stosb
           //@2:     pop     es
           //        in      al, 61h
           //        push    ax
           //        or      al, 80h
           //        out     61h, al
           //        pop     ax
           //        out     61h, al
           //        cli
           //        mov     al, 20h
           //        out     20h, al
           //        sti

           //        pop     di
           //        pop     dx
           //        pop     cx
           //        pop     ax
           //        popf
           //        iret
           //end;
           //{$F-}
        }

        public void InitKeyBoard()
        {
           //  var
           //    i: Integer;
           //begin
           //  Port[$60] := $ED;
           //  for i := 1 to 1000 do ;
           //  Port[$60] := 0;
           //  OldExitProc := ExitProc;
           //  GetIntVec($09, @OldKbIntVec);
           //  ExitProc := @NewExitProc;
           //  SetIntVec($09, Addr(GetKey));
           //  HandlerActive := TRUE;
           //  KeyHit := FALSE;
           //end;
        }

        public void KeyBoardDone()
        {
              //begin
              //  if not HandlerActive then
              //    Exit;
              //  SetIntVec($09, @OldKbIntVec);
              //  HandlerActive := FALSE;
              //  Mem[$0:$417] := 0;
              //end;
        }

        public void ResetKeyBoard()
        {
              //var
              //  i: Byte;
              //begin
              //  Recording := FALSE;
              //  Playing := FALSE;
              //  for i := 1 to MaxKeys
              //  do
              //    KeyMap[i] := False;
              //  Key := #0;
              //end;
        }

        public void RecordMacro()
        {
            //  begin
            //    Recording := TRUE;
            //    Playing := FALSE;
            //    FillChar (SeqPos, sizeof (SeqPos), 0);
            //    FillChar (Sequence, sizeof (SeqPos), 0);
            //    RandSeed := 0;
            //  end;

            //  procedure PlayMacro;
            //  begin
            //    Playing := TRUE;
            //    Recording := FALSE;
            //    FillChar (SeqPos, sizeof (SeqPos), 0);
            //    Move (@Macro^, Sequence, sizeof (Sequence));
            //{    FillChar (Sequence, sizeof (SeqPos), 0); }
            //    RandSeed := 0;
            //  end;
        }

        public void StopMacro()
        {
           Playing = false;
           Recording = false;
        }

        public void SaveMacro()
        {
              //  var
              //    F: File of KeySeq;
              //    i: Integer;
              //begin
              //  Assign (F, '$');
              //  ReWrite (F);
              //  for i := 1 to MaxKeys do
              //    Write (F, Sequence[i]);
              //  Close (F);

              //  Recording := FALSE;
              //  FillChar (SeqPos, sizeof (SeqPos), 0);
              //end;
        }

        public bool PlayingMacro()
        {
           return Playing;
        }

        public bool Check(byte KeyNr, bool Press)
        {
           return false;
              //begin
              //  Check := Press;
              //  if Playing or Recording then
              //  begin
              //    if Recording then
              //    begin
              //      if Press xor (SeqPos[KeyNr] mod 2 = 1) then
              //      begin
              //        Inc (SeqPos[KeyNr]);
              //        if SeqPos[KeyNr] >= MAX_SEQ_LEN then
              //          SeqPos[KeyNr] := MAX_SEQ_LEN - 1;
              //      end;
              //      Inc (Sequence[KeyNr, SeqPos[KeyNr]]);
              //    end;
              //    if Playing then
              //    begin
              //      if Sequence[KeyNr, SeqPos[KeyNr]] = 0 then
              //        Playing := FALSE
              //      else
              //      begin
              //        Dec (Sequence[KeyNr, SeqPos[KeyNr]]);
              //        if Sequence[KeyNr, SeqPos[KeyNr]] = 0 then
              //          Inc (SeqPos[KeyNr]);
              //        Check := (SeqPos[KeyNr] mod 2 = 1);
              //      end;
              //    end;
              //  end;
              //end;
        }

        public bool kbHit()
        {
           KeyHit = false;
           return KeyHit;
        }

        public bool kbLeft()
        {
           bool kbLeft = Check(keyLeft, KeyMap[keyLeft]);
           return kbLeft;
        }

        public bool kbRight()
        {
           bool kbRight = Check (keyRight, KeyMap[keyRight]);
           return kbRight;
        }

        public bool kbUp()
        {
           bool kbUp = Check (keyUp, KeyMap[keyUp]);
           return kbUp;
        }

        public bool kbDown()
        {
           bool kbDown = Check (keyDown, KeyMap[keyDown]);
           return kbDown;
        }

        public bool kbAlt()
        {
           bool kbAlt = Check (keyAlt, KeyMap[keyAlt]);
           return kbAlt;
        }

        public bool kbCtrl()
        {
           bool kbCtrl = Check(keyCtrl, KeyMap[keyCtrl]);
           return kbCtrl;
        }

        public bool kbLeftShift()
        {
           bool kbLeftShift = Check (keyShiftL, KeyMap[keyShiftL]);
           return kbLeftShift;
        }

        public bool kbRightShift()
        {
           bool kbRightShift = Check (keyShiftR, KeyMap[keyShiftR]);
           return kbRightShift;
        }

        public bool kbSpace()
        {
           bool kbSpace = Check (keySpace, KeyMap[keySpace]);
           return kbSpace;
        }

        public char GetAsciiCode(char c)
        {
            //    const
            //      kbTable: array[0..3] of string[10] =
            //        ('1234567890',
            //         'QWERTYUIOP',
            //         'ASDFGHJKL',
            //         'ZXCVBNM');
            //    var
            //      i: Byte absolute c;
            //  begin
            //    case i of
            //       2..11: GetAsciiCode := kbTable[0, i - 2 + 1];
            //      16..25: GetAsciiCode := kbTable[1, i - 16 + 1];
            //      30..38: GetAsciiCode := kbTable[2, i - 30 + 1];
            //      44..50: GetAsciiCode := kbTable[3, i - 44 + 1];
            //    else
            //      GetAsciiCode := #0;
            //    end
            //  end;

            //end.
           return c;
        }
   }
}
