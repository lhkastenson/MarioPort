using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MarioPort
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //const VGA_SEGMENT = $A000;  do not think we will need this
        const int windowHeight = 13 * 14;
        const int windowWidth = 16 * 20;
        public const int screenWidth = 320;        //same as windowWidth?
        const int screenHeight = 200;       //same as windowHeight?
        const int virScreenWidth = screenWidth + 2 * 20;
        const int virScreenHeight = 182;
        const int bytesPerLine = virScreenWidth / 4;
        
        /*MISC_OUTPUT         = $03C2;
        SC_INDEX            = $03C4;
        GC_INDEX            = $03CE;
        CRTC_INDEX          = $03D4;
        VERT_RESCAN         = $03DA;*/

        const int mapMask = 2;
        const int memoryMode = 4;
        const int vert_retraceMask = 8;
        const int maxScanLine = 9;

        byte START_ADDRESS_HIGH  = 11;
        byte START_ADDRESS_LOW   = 12;
        byte UNDERLINE           = 20;
        byte MODE_CONTROL        = 23;

        const int readMap = 4;
        const int graphicsMode = 5;
        const int misc = 6;

        const int maxScreens = 24;
        public const int maxPage = 1;
        const int pageSize = (virScreenHeight + maxScreens) * bytesPerLine;

        /*PAGE_0              = 0;
        PAGE_1              = $8000;*/
        byte page0 = 0;
        uint page1 = 32768;
        
        const int yBase = 9;
        public bool InGraphicsMode = false; 
        public static int xView = 0;
        public static int yView = 0;
        int page = 0;
        ushort pageOffset = 0;
        int yOffset = 0;
        const int safe = 34 * bytesPerLine;

        /*Stack: array[0..MAX_PAGE] of Word =
        (PAGE_0 + PAGE_SIZE + SAFE,
        PAGE_1 + PAGE_SIZE + SAFE);*/
        //?????????????????????????????????????????
    

        //Var
        byte OldScreenMode;
        //OldExitProc: Pointer;
        
        //{ Be sure to return to textmode if program is halted }
        public void newExitProc()
        {
            /*OldMode;
            ExitProc := OldExitProc;*/
        }
        
        //{ Set screen width (NewWidth >= 40) }
        public void setWidth(ushort newWidth)
        {
            /*asm
               mov     ax, NewWidth
               push    ax
               mov     dx, CRTC_INDEX
               mov     ax, 13h
               out     dx, al
               pop     ax
               inc     dx
               out     dx, al
            end;*/
        }
        
        // function DetectVGA: Boolean;
        public bool DetectVGA()
        {
            bool VGADetected = false;
            /*asm
               push    bp
               mov     ax, 1A00h
               int     10h
               cmp     al, 1Ah
               jnzf     @NoVGA
               inc     VGADetected
            @NoVGA:
               pop     bp
            end;
            DetectVGA := VGADetected;*/
            return true;
        }

        //procedure InitVGA;
        //{ Start graphics mode 320x200 256 colors }
        public void InitializeVGA()
        {
            /*ClearPalette;
            SetMode ($13);
            ClearPalette;
            SetWidth (BYTES_PER_LINE shr 1);
            asm
               mov     dx, SC_INDEX
               mov     al, MEMORY_MODE
               out     dx, al
               inc     dx
               in      al, dx
               and     al, not 8
               or      al, 4
               out     dx, al
               mov     dx, GC_INDEX
               mov     al, GRAPHICS_MODE
               out     dx, al
               inc     dx
               in      al, dx
               and     al, not 10h
               out     dx, al
               dec     dx
               mov     al, MISCELLANEOUS
               out     dx, al
               inc     dx
               in      al, dx
               and     al, not 2
               out     dx, al
            end;
            asm
               mov     dx, CRTC_INDEX
               mov     al, UNDERLINE
               out     dx, al
               inc     dx
               in      al, dx
               and     al, not 40h
               out     dx, al
               dec     dx
               mov     al, MODE_CONTROL
               out     dx, al
               inc     dx
               in      al, dx
               or      al, 40h
               out     dx, al
            end;
            if not InGraphicsMode then
            begin
               OldExitProc := ExitProc;
               ExitProc := @NewExitProc;
            end;
            InGraphicsMode := TRUE;*/
        }

        //procedure OldMode;
        //{ Return to the original screenmode }
        public void OldMode()
        {
            //if (InGraphicsMode)
            //{
            //   ClearVGAMem();
            //   ClearPalette();
            //   ShowPage();
            //}
            
            //SetMode (OldScreenMode);
            //InGraphicsMode = false;
            ////ExitProc = OldExitProc;
        }

        //function GetMode: Byte;
        // get video mode
        public byte GetMode()
        {
            /*asm
               push    bp
               mov     ah, 0Fh
               int     10h
               mov     @Result, al
               pop     bp
            end;*/
            
            return 1;
        }
        
        //procedure SetMode (NewMode: Byte);
        // set video mode
        public void SetMode(byte newMode)
        {
            /*asm
               push    bp
               xor     ah, ah
               mov     al, NewMode
               int     10h
               pop     bp
            end;*/
            
        }

        //procedure ClearVGAMem;
        
        public void ClearVGAMem()
        {
            /*asm
               push    es
               mov     dx, SC_INDEX
               mov     ax, 0F00h + MAP_MASK
               out     dx, ax
               mov     ax, VGA_SEGMENT
               mov     es, ax
               xor     ax, ax
               mov     di, ax
               mov     cx, 8000h
               cld
               rep     stosw
               pop     es
            end;*/
        }

        //procedure WaitDisplay;
        public void WaitDisplay()
        {
            /*asm
               mov     dx, VERT_RESCAN
       @1:     in      al, dx
               test    al, VERT_RETRACE_MASK
               jnz     @1
            end;*/
        }

        //procedure WaitRetrace;
        public void WaitRetrace()
        {
            /*asm
               mov     dx, VERT_RESCAN
         @1:   in      al, dx
               test    al, VERT_RETRACE_MASK
               jz      @1
            end;*/
        }

        //procedure SetView (X, Y: Integer);
        public void SetView(int x, int y)
        {
            xView = x;
            yView = y;
        }

        //procedure SetViewport (X, Y: Integer; PageNr: Byte);
        //{ Set the offset of video memory }
        public void SetViewport(int x, int y, byte pgNum)
        {
            int i;
            /*asm
               cli

               mov     dx, VERT_RESCAN               { wait for display }
       @1:     in      al, dx
               test    al, VERT_RETRACE_MASK
               jnz     @1
      
               shl     X, 1
               shl     Y, 1
               mov     ax, Y
               mov     bx, BYTES_PER_LINE / 2
               mul     bx
               mov     bx, X
               mov     cl, 3
               shr     bx, cl
               add     bx, ax
               mov     al, START_ADDRESS_HIGH
               mov     ah, PageNr
               ror     ah, 1
               add     ah, bh
               mov     dx, CRTC_INDEX
               out     dx, ax
               mov     al, START_ADDRESS_LOW
               mov     ah, bl
               out     dx, ax
      
               mov     dx, VERT_RESCAN               { wait for retrace }
      @2:     in      al, dx
               test    al, VERT_RETRACE_MASK
               jz      @2
      
               mov     ax, X
               and     ax, 7
               add     al, 10h
               mov     dx, 3c0h
               mov     ah, al
               mov     al, 33h
               out     dx, al
               xchg    ah, al
               out     dx, al
               sti
            end;*/
        }

        //procedure SwapPages;
        public void SwapPages()
        {
            if ( page == 0)
            {
               page = 1;
               pageOffset = (ushort)(page1 + yOffset * bytesPerLine);
            }
            else if(page == 1)
            {
               page = 0;
               pageOffset = (ushort)(page0 + yOffset * bytesPerLine);
            }
        }

        //procedure ShowPage;
        public void ShowPage()
        {
            SetViewport (xView, yView, (byte)(page));
            SwapPages();
        }

        //procedure Border (Attr: Byte);
        //{ Draw a border around the screen }
        public void Border(/*byte attr*/)
        {
            /*asm
               push    bp
               mov     ax, 1001h
               mov     bh, Attr
               int     10h
               pop     bp
            end;*/
        }

        //procedure SetYStart (NewYStart: Integer);
        public void SetYStart(int yStart)
        {
            /*asm
               mov     dx, CRTC_INDEX
               mov     al, 16h
               mov     ah, Byte Ptr [NewYStart]
               and     ah, 7Fh
               out     dx, ax
            end;*/
        }

        //procedure SetYEnd (NewYEnd: Integer);
        public void SetYEnd(int yEnd)
        {
            /*asm
               mov     dx, CRTC_INDEX
               mov     al, 15h
               mov     ah, Byte Ptr [NewYEnd]
               out     dx, ax
            end;*/
        }

        //procedure SetYOffset (NewYOffset: Integer);
        public void SetYOffset(int newYOffset)
        {
            yOffset = newYOffset;
        }

        //function GetYOffset: Integer;
        public int GetYOffset()
        {
            return yOffset;
        }

        //procedure PutPixel (X, Y: Integer; Attr: Byte);
        //{ Draw a single pixel at (X, Y) with color Attr }
        public void PutPixel(int x, int y/*, byte attr*/)
        {
            /*asm
               push    es
               mov     ax, VGA_SEGMENT
               mov     es, ax
               mov     dx, Y
               mov     ax, BYTES_PER_LINE
               mul     dx
               mov     cx, X
               push    cx
               shr     cx, 1
               shr     cx, 1
               add     ax, cx
               mov     di, ax
               add     di, PageOffset
               pop     cx
               and     cl, 3
               mov     ah, 1
               shl     ah, cl
               mov     al, MAP_MASK
               mov     dx, SC_INDEX
               out     dx, ax
               mov     al, Attr
               stosb
               pop     es
            end;*/
        }

        //function GetPixel (X, Y: Integer): Byte;
        //{ Get color of pixel at (X, Y) }
        public byte GetPixel(int x, int y)
        {
            /*asm
               push    es
               mov     ax, VGA_SEGMENT
               mov     es, ax
               mov     dx, Y
               mov     ax, BYTES_PER_LINE
               mul     dx
               mov     cx, X
               push    cx
               shr     cx, 1
               shr     cx, 1
               add     ax, cx
               mov     si, ax
               add     si, PageOffset
               pop     ax
               and     al, 3
               mov     ah, al
               mov     al, READ_MAP
               mov     dx, GC_INDEX
               out     dx, ax
               seges   mov al, [si]
               pop     es
               mov     @Result, al
            end;*/
            return 1;
        }

        //procedure DrawImage (XPos, YPos, Width, Height: Integer; var BitMap);
        //{ Draw an image on the screen (NULL-bytes are ignored) }
        public void DrawImage(int xPos, int yPos, int width, int height /*, BitMap var*/)
        {
            /*asm
               push    ds
         
               mov     ax, VGA_SEGMENT
               mov     es, ax
         
               mov     ax, YPos
               cmp     ax, VIR_SCREEN_HEIGHT
               jb      @NotNeg
               jg      @End
               mov     bx, ax
               add     bx, Height
               jnc     @End
         @NotNeg:
               mov     bx, BYTES_PER_LINE
               mul     bx
               mov     di, XPos
               mov     bx, di
               shr     di, 1
               shr     di, 1
               add     di, ax                  { DI = (YPos * 80) + XPos / 4 }
               add     di, PageOffset
         
               lds     si, BitMap              { Point to bitmap }

               and     bl, 3
               mov     cl, bl
               mov     ah, 1
               shl     ah, cl
               sub     bl, 4
               mov     cx, 4                   { 4 planes }

         @Plane:
               push    bx
               push    cx                      { Planes to go }
               push    ax                      { Mask in AH }
         
               mov     al, MAP_MASK
               mov     dx, SC_INDEX
               out     dx, ax

               cld
               push    di
               mov     bx, Width
               shr     bx, 1
               shr     bx, 1
               mov     ax, BYTES_PER_LINE
               sub     ax, bx                  { Space before next line }
               mov     dx, Height
         @Line:
               mov     cx, bx
               shr     cx, 1
   
               push    ax
               pushf

         @Pixel:
               lodsw
               or      al, al
               jz      @Skip1
               seges
               mov     [di], al
         @Skip1:
               inc     di
               or      ah, ah
               jz      @Skip2
               seges
               mov     [di], ah
         @Skip2:
               inc     di
               loop    @Pixel
      
               popf
               rcl     cx, 1
               jcxz    @Skip3
   
               lodsb
               or      al, al
               jz      @Odd
               stosb
               jmp     @Skip3
         @Odd: inc     di
         @Skip3:
               pop     ax
               add     di, ax
               dec     dx
               jnz     @Line

               pop     di
      
               pop     ax
               mov     al, ah
               mov     cl, 4
               shl     al, cl
               or      ah, al                  { Mask for next byte }
               rol     ah, 1                   { Bit mask for next plane }
               pop     cx                      { Planes }
               pop     bx
               inc     bl                      { Still in the same byte? }
               adc     di, 0
               loop    @Plane

         @End:
               pop     ds      
            end;*/
        }

        //procedure RecolorImage (XPos, YPos, Width, Height: Integer; var BitMap; Diff: Byte);
        public void RecolorImage(int xPos, int yPos, int width, int height /*BitMap var*/, byte diff)
        {
            /*asm
                 push    ds

                 mov     ax, VGA_SEGMENT
                 mov     es, ax

                 mov     ax, YPos
                 cmp     ax, VIR_SCREEN_HEIGHT
                 jb      @NotNeg
                 jg      @End
                 mov     bx, ax
                 add     bx, Height
                 jnc     @End
           @NotNeg:
                 mov     bx, BYTES_PER_LINE
                 mul     bx
                 mov     di, XPos
                 mov     bx, di
                 shr     di, 1
                 shr     di, 1
                 add     di, ax                  { DI = (YPos * 80) + XPos / 4 }
                 add     di, PageOffset

                 lds     si, BitMap              { Point to bitmap }

                 and     bl, 3
                 mov     cl, bl
                 mov     ah, 1
                 shl     ah, cl
                 sub     bl, 4
                 mov     cx, 4                   { 4 planes }

           @Plane:
                 push    bx
                 push    cx                      { Planes to go }
                 push    ax                      { Mask in AH }

                 mov     al, MAP_MASK
                 mov     dx, SC_INDEX
                 out     dx, ax

                 cld
                 push    di
                 mov     bx, Width
                 shr     bx, 1
                 shr     bx, 1
                 mov     ax, BYTES_PER_LINE
                 sub     ax, bx                  { Space before next line }
                 mov     dx, Height
           @Line:
                 mov     cx, bx
                 shr     cx, 1

                 push    ax
                 pushf

           @Pixel:
                 lodsw
                 or      al, al
                 jz      @Skip1
                 add     al, Diff
                 seges
                 mov     [di], al
           @Skip1:
                 inc     di
                 or      ah, ah
                 jz      @Skip2
                 add     ah, Diff
                 seges
                 mov     [di], ah
           @Skip2:
                 inc     di
                 loop    @Pixel

                 popf
                 rcl     cx, 1
                 jcxz    @Skip3

                 lodsb
                 or      al, al
                 jz      @Odd
                 add     al, Diff
                 stosb
                 jmp     @Skip3
           @Odd: inc     di
           @Skip3:
                 pop     ax
                 add     di, ax
                 dec     dx
                 jnz     @Line

                 pop     di

                 pop     ax
                 mov     al, ah
                 mov     cl, 4
                 shl     al, cl
                 or      ah, al                  { Mask for next byte }
                 rol     ah, 1                   { Bit mask for next plane }
                 pop     cx                      { Planes }
                 pop     bx
                 inc     bl                      { Still in the same byte? }
                 adc     di, 0
                 loop    @Plane

             @End:
                 pop     ds
             end;*/
        }

        //procedure DrawPart (XPos, YPos, Width, Height, Y1, Y2: Integer; var BitMap);
        public void DrawPart(int xPos, int yPos, int width, int height, int y1, int y2 /*,BitMap var*/)
        {
            /*asm
                 push    ds
                 cmp     Height, 0
                 jle     @End

                 mov     ax, VGA_SEGMENT
                 mov     es, ax

                 mov     ax, YPos
                 cmp     ax, VIR_SCREEN_HEIGHT
                 jb      @NotNeg
                 jg      @End
                 mov     bx, ax
                 add     bx, Height
                 jnc     @End
           @NotNeg:
                 mov     bx, BYTES_PER_LINE
                 mul     bx
                 mov     di, XPos
                 mov     bx, di
                 shr     di, 1
                 shr     di, 1
                 add     di, ax                  { DI = (YPos * 80) + XPos / 4 }
                 add     di, PageOffset

                 lds     si, BitMap              { Point to bitmap }

                 and     bl, 3
                 mov     cl, bl
                 mov     ah, 1
                 shl     ah, cl
                 sub     bl, 4
                 mov     cx, 4                   { 4 planes }

           @Plane:
                 push    bx
                 push    cx                      { Planes to go }
                 push    ax                      { Mask in AH }

                 mov     al, MAP_MASK
                 mov     dx, SC_INDEX
                 out     dx, ax

                 cld
                 push    di
                 mov     bx, Width
                 shr     bx, 1
                 shr     bx, 1
                 mov     ax, BYTES_PER_LINE
                 sub     ax, bx                  { Space before next line }

                 xor     dx, dx
           @Line:
                 cmp     dx, Y1
                 jl      @EndLine
                 cmp     dx, Y2
                 jg      @EndLine

                 mov     cx, bx
                 shr     cx, 1

                 push    ax
                 pushf

           @Pixel:
                 lodsw
                 or      al, al
                 jz      @Skip1
                 seges
                 mov     [di], al
           @Skip1:
                 inc     di
                 or      ah, ah
                 jz      @Skip2
                 seges
                 mov     [di], ah
           @Skip2:
                 inc     di
                 loop    @Pixel

                 popf
                 rcl     cx, 1
                 jcxz    @Skip3

                 lodsb
                 or      al, al
                 jz      @Odd
                 stosb
                 jmp     @Skip3
           @Odd: inc     di
           @Skip3:
                 pop     ax
                 add     di, ax
                 jmp     @1

           @EndLine:
                 add     si, bx
                 add     di, BYTES_PER_LINE

           @1:   inc     dx
                 cmp     dx, Height
                 jb      @Line

                 pop     di

                 pop     ax
                 mov     al, ah
                 mov     cl, 4
                 shl     al, cl
                 or      ah, al                  { Mask for next byte }
                 rol     ah, 1                   { Bit mask for next plane }
                 pop     cx                      { Planes }
                 pop     bx
                 inc     bl                      { Still in the same byte? }
                 adc     di, 0
                 loop    @Plane

           @End:
                 pop     ds
             end;*/
        }

        //procedure UpSideDown (XPos, YPos, Width, Height: Integer; var BitMap);
        //{ Draw an image on the screen up-side-down (NULL-bytes are ignored) }
        public void UpSideDown(int xPos, int yPos, int width, int height /*BitMap var*/)
        {
            /*asm
                 push    ds

                 mov     ax, VGA_SEGMENT
                 mov     es, ax

                 mov     ax, YPos
                 cmp     ax, VIR_SCREEN_HEIGHT
                 jb      @NotNeg
                 jg      @End
                 mov     bx, ax
                 add     bx, Height
                 jnc     @End
           @NotNeg:
                 add     ax, Height
                 dec     ax
                 mov     bx, BYTES_PER_LINE
                 mul     bx
                 mov     di, XPos
                 mov     bx, di
                 shr     di, 1
                 shr     di, 1
                 add     di, ax                  { DI = (YPos * 80) + XPos / 4 }
                 add     di, PageOffset

                 lds     si, BitMap              { Point to bitmap }

                 and     bl, 3
                 mov     cl, bl
                 mov     ah, 1
                 shl     ah, cl
                 sub     bl, 4
                 mov     cx, 4                   { 4 planes }

           @Plane:
                 push    bx
                 push    cx                      { Planes to go }
                 push    ax                      { Mask in AH }

                 mov     al, MAP_MASK
                 mov     dx, SC_INDEX
                 out     dx, ax

                 cld
                 push    di
                 mov     bx, Width
                 shr     bx, 1
                 shr     bx, 1
                 mov     ax, BYTES_PER_LINE
                 add     ax, bx                  { Space before next line }
                 mov     dx, Height
           @Line:
                 mov     cx, bx
                 shr     cx, 1

                 push    ax
                 pushf

           @Pixel:
                 lodsw
                 or      al, al
                 jz      @Skip1
                 seges
                 mov     [di], al
           @Skip1:
                 inc     di
                 or      ah, ah
                 jz      @Skip2
                 seges
                 mov     [di], ah
           @Skip2:
                 inc     di
                 loop    @Pixel

                 popf
                 rcl     cx, 1
                 jcxz    @Skip3

                 lodsb
                 or      al, al
                 jz      @Odd
                 stosb
                 jmp     @Skip3
           @Odd: inc     di
           @Skip3:
                 pop     ax
                 sub     di, ax
                 dec     dx
                 jnz     @Line

                 pop     di

                 pop     ax
                 mov     al, ah
                 mov     cl, 4
                 shl     al, cl
                 or      ah, al                  { Mask for next byte }
                 rol     ah, 1                   { Bit mask for next plane }
                 pop     cx                      { Planes }
                 pop     bx
                 inc     bl                      { Still in the same byte? }
                 adc     di, 0
                 loop    @Plane
           @End:
                 pop     ds
             end;*/
        }

        //procedure PutImage (XPos, YPos, Width, Height: Integer; var BitMap);
        //{ Draw an image on the screen (NULL-bytes are NOT ignored) }
        public void PutImage(int xPos, int yPos, int width, int height/*, Texture var*/)
        {
            /*asm
                 push    ds
                 push    es
                 mov     ax, VGA_SEGMENT
                 mov     es, ax

                 mov     ax, YPos
                 mov     bx, BYTES_PER_LINE
                 mul     bx
                 mov     di, XPos
                 mov     bx, di
                 shr     di, 1
                 shr     di, 1
                 add     di, ax                  { DI = (YPos * 80) + XPos / 4 }
                 add     di, PageOffset

                 lds     si, BitMap              { Point to bitmap }

                 and     bl, 3
                 mov     cl, bl
                 mov     ah, 1
                 shl     ah, cl
                 sub     bl, 4
                 mov     cx, 4                   { 4 planes }

           @Plane:
                 push    bx
                 push    cx                      { Planes to go }
                 push    ax                      { Mask in AH }

                 mov     al, MAP_MASK
                 mov     dx, SC_INDEX
                 out     dx, ax

                 cld
                 push    di
                 mov     bx, Width
                 shr     bx, 1
                 shr     bx, 1
                 mov     ax, BYTES_PER_LINE
                 sub     ax, bx                  { Space before next line }
                 mov     dx, Height
           @Line:
                 mov     cx, bx
                 shr     cx, 1
                 rep     movsw
                 rcl     cx, 1
                 rep     movsb
                 add     di, ax
                 dec     dx
                 jnz     @Line

                 pop     di

                 pop     ax
                 mov     al, ah
                 mov     cl, 4
                 shl     al, cl
                 or      ah, al                  { Mask for next byte }
                 rol     ah, 1                   { Bit mask for next plane }
                 pop     cx                      { Planes }
                 pop     bx
                 inc     bl                      { Still in the same byte? }
                 adc     di, 0
                 loop    @Plane


                 pop     es
                 pop     ds
             end;*/
        }

        //procedure GetImage (XPos, YPos, Width, Height: Integer; var BitMap);
        public void GetImage(int xPos, int yPos, int width, int height /*, BitMap var*/)
        {
            /*asm
                 push    ds
                 push    es

                 mov     cx, PageOffset

                 mov     ax, VGA_SEGMENT
                 mov     ds, ax

                 mov     ax, YPos
                 mov     bx, BYTES_PER_LINE
                 mul     bx
                 mov     si, XPos
                 mov     bx, si
                 shr     si, 1
                 shr     si, 1
                 add     si, ax                  { SI = (YPos * 80) + XPos / 4 }
                 add     si, cx

                 les     di, BitMap              { Point to bitmap }

                 and     bl, 3
                 sub     bl, 4
                 mov     cx, 4                   { 4 planes }

           @Plane:
                 push    bx
                 push    cx                      { Planes to go }

                 mov     ah, bl
                 and     ah, 3
                 mov     al, READ_MAP
                 mov     dx, GC_INDEX
                 out     dx, ax

                 cld
                 push    si
                 mov     bx, Width
                 shr     bx, 1
                 shr     bx, 1
                 mov     ax, BYTES_PER_LINE
                 sub     ax, bx                  { Space before next line }
                 mov     dx, Height
           @Line:
                 mov     cx, bx
                 shr     cx, 1
                 rep     movsw
                 rcl     cx, 1
                 rep     movsb
                 add     si, ax
                 dec     dx
                 jnz     @Line

                 pop     si

                 pop     cx                      { Planes }
                 pop     bx
                 inc     bl                      { Still in the same byte? }
                 adc     si, 0
                 loop    @Plane


                 pop     es
                 pop     ds
             end;*/
        }

        //procedure Fill (X, Y, W, H: Integer; Attr: Integer);
        //{ Fills an area on the screen with Attr }
        public void Fill(int x, int y, int width, int height/*, int attr*/)
        {  
            /*asm
                 mov     ax, VGA_SEGMENT
                 mov     es, ax

                 cld
                 mov     dx, Y
                 mov     ax, BYTES_PER_LINE
                 mul     dx
                 mov     di, X
                 push    di
                 shr     di, 1
                 shr     di, 1
                 add     di, ax                  { DI = Y * (width / 4) + X / 4 }
                 add     di, PageOffset
                 pop     cx
                 and     cx, 3                   { CX = X mod 4 }

                 mov     ah, 0Fh
                 shl     ah, cl
                 and     ah, 0Fh

                 mov     si, H
                 or      si, si
                 jz      @End                    { Height 0 }
                 mov     bh, byte ptr Attr
                 mov     dx, W
                 or      dx, dx
                 jz      @End                    { Width 0 }
                 add     cx, dx
                 mov     dx, SC_INDEX
                 mov     al, MAP_MASK
                 sub     cx, 4
                 jc      @2
                 test    cl, 3h
                 jnz     @0
                 sub     cx, 4
           @0:   jc      @2
                 out     dx, ax

                 mov     al, bh                  { Attr }
                 push    si                      { Height }
                 push    di
           @4:   stosb                           { Left vertical line }
                 add     di, BYTES_PER_LINE - 1
                 dec     si
                 jnz     @4
                 pop     di
                 inc     di
                 pop     si

                 push    ax
                 mov     ax, 0F00h + MAP_MASK
                 out     dx, ax
                 pop     ax

                 mov     ah, al                  { Attr }
                 push    cx                      { Width }
                 shr     cx, 1
                 shr     cx, 1

                 push    si                      { Height }
                 push    di
           @5:   push    di
                 push    cx
                 shr     cx, 1
                 rep     stosw                   { Fill middle part }
                 rcl     cx, 1
                 rep     stosb
                 pop     cx
                 pop     di
                 add     di, BYTES_PER_LINE
                 dec     si
                 jnz     @5
                 pop     di
                 add     di, cx                  { Point to last strip }
                 pop     si                      { Height }

                 pop     cx                      { Width }
                 mov     bh, al                  { Attr }
                 mov     bl, 0Fh                 { Mask }
                 jmp     @3

           @2:   mov     bl, ah                  { Begin and end in one single byte }

           @3:   and     cl, 3
                 mov     ah, 0
           @1:   shl     ah, 1
                 add     ah, 1
                 dec     cl
                 jnz     @1

                 and     ah, bl                  { Use both masks }
                 mov     al, MAP_MASK
                 out     dx, ax
                 mov     al, bh                  { Attr }
           @6:   stosb                           { Draw right vertical line }
                 add     di, BYTES_PER_LINE - 1
                 dec     si
                 jnz     @6
           @End:
             end;*/
        }

        //procedure SetPalette (Color, Red, Green, Blue: Byte);
        public void setPalete(byte color, byte red, byte green, byte blue)
        {
            /*asm
                   mov     dx, 03C8h       { DAC Write Address Register }
                   mov     al, Color
                   out     dx, al
                   inc     dx
                   mov     al, Red
                   out     dx, al
                   mov     al, Green
                   out     dx, al
                   mov     al, Blue
                   out     dx, al
             end;*/
        }

        //procedure ReadPalette (var NewPalette);
        //{ Read whole palette }
        public void ReadPalette (/*NewPallete var*/)
        {
            /*asm
                 push    ds
                 lds     si, NewPalette
                 mov     dx, 3C8h        { VGA pel address }
                 mov     al, 0
                 cli
                 cld
                 out     dx, al
                 inc     dx
                 mov     cx, 3 * 100h
           @1:   lodsb
                 out     dx, al
                 dec     cx
                 jnz     @1
                 sti
                 pop     ds
            end;*/
        }

        //procedure ClearPalette;
        public void ClearPalette()
        {
            /*asm
                 cli
                 mov     dx, 3C8h        { VGA pel address }
                 mov     al, 0
                 out     dx, al
                 inc     dx
                 mov     cx, 3 * 100h
           @1:   out     dx, al
                 dec     cx
                 jnz     @1
                 sti
            end;*/
        }

        //function CurrentPage: Integer;
        public int CurrentPage()
        {
            return page;
        }

        //function GetPageOffset: Word;
        public ushort GetPageOffset()
        {
            return pageOffset;
        }

        //procedure ResetStack;
        public void ResetStack()
        {
            /*Stack[0] := PAGE_0 + PAGE_SIZE + SAFE;
            Stack[1] := PAGE_1 + PAGE_SIZE + SAFE;*/
        }

        //function PushBackGr (X, Y, W, H: Integer): Word;
        public ushort PushBackGr(int x, int y, int w, int h)
        {
            Stack stackPtr new Stack<ushort> [];
            if (!((y + h >= 0) && (y < 200)))
               return 0;
            stackPtr = new Stack<ushort> [];

             /*StackPointer = Stack [Page];
             asm
                 mov     bx, PageOffset
                 mov     di, StackPointer
                 push    ds
                 push    es

                 mov     ax, VGA_SEGMENT
                 mov     ds, ax
                 mov     es, ax

                 cld
                 mov     dx, SC_INDEX
                 mov     ax, 0100h + MAP_MASK
                 out     dx, ax
                 mov     ax, X
                 mov     [di], ax
                 mov     ax, 0200h + MAP_MASK
                 out     dx, ax
                 mov     ax, Y
                 mov     [di], ax
                 mov     ax, 0400h + MAP_MASK
                 out     dx, ax
                 mov     ax, W
                 mov     [di], ax
                 mov     ax, 0800h + MAP_MASK
                 out     dx, ax
                 mov     ax, H
                 stosw
                 mov     al, 'M'
                 stosb

                 mov     dx, GC_INDEX
                 mov     al, GRAPHICS_MODE
                 out     dx, al
                 inc     dx
                 in      al, dx
                 push    ax
                 mov     al, 41h
                 out     dx, al

                 mov     dx, SC_INDEX
                 mov     ax, 0F00h + MAP_MASK
                 out     dx, ax

                 mov     ax, READ_MAP
                 mov     dx, GC_INDEX
                 out     dx, ax

                 mov     dx, Y
                 mov     ax, BYTES_PER_LINE
                 mul     dx
                 mov     si, X
                 shr     si, 1
                 shr     si, 1
                 add     si, ax
                 add     si, bx

                 mov     cx, W
                 shr     cx, 1
                 shr     cx, 1

                 mov     bx, H

           @1:   push    cx
                 rep
                 movsb                   { copy 4 pixels }
                 pop     cx
                 add     si, BYTES_PER_LINE
                 sub     si, cx
                 dec     bx
                 jnz     @1

                 mov     dx, GC_INDEX
                 pop     ax
                 mov     ah, al
                 mov     al, GRAPHICS_MODE
                 out     dx, ax

                 pop     es
                 pop     ds
             end;
             PushBackGr := Stack [Page];
             Inc (Stack [Page], W * H + 8);
           end;*/
            return 1;
        }

        //procedure PopBackGr (Address: Word);
        public void PopBackGr(ushort address)
        {
            int x, y, w, h;
            
            /*if Address = 0 then
               Exit;
             asm
                 mov     bx, PageOffset
                 mov     si, Address

                 push    ds
                 push    es

                 mov     ax, VGA_SEGMENT
                 mov     ds, ax
                 mov     es, ax

                 cld
                 mov     dx, GC_INDEX
                 mov     ax, 0000h + READ_MAP
                 out     dx, ax
                 mov     ax, [si]
                 mov     X, ax
                 mov     ax, 0100h + READ_MAP
                 out     dx, ax
                 mov     ax, [si]
                 mov     Y, ax
                 mov     ax, 0200h + READ_MAP
                 out     dx, ax
                 mov     ax, [si]
                 mov     W, ax
                 mov     ax, 0300h + READ_MAP
                 out     dx, ax
                 lodsw
                 mov     H, ax
                 lodsb
                 cmp     al, 'M'
                 jz      @@1
         {$IFDEF DEBUG}
                 int     3
         {$ENDIF}
                 jmp     @End
             @@1:
                 mov     dx, GC_INDEX
                 mov     al, GRAPHICS_MODE
                 out     dx, al
                 inc     dx
                 in      al, dx
                 push    ax
                 mov     al, 41h
                 out     dx, al

                 mov     dx, SC_INDEX
                 mov     ax, 0F00h + MAP_MASK
                 out     dx, ax

                 mov     ax, READ_MAP
                 mov     dx, GC_INDEX
                 out     dx, ax

                 mov     dx, Y
                 mov     ax, BYTES_PER_LINE
                 mul     dx
                 mov     di, X
                 shr     di, 1
                 shr     di, 1
                 add     di, ax
                 add     di, bx

                 mov     cx, W
                 shr     cx, 1
                 shr     cx, 1

                 mov     bx, H

           @1:   push    cx
                 rep
                 movsb                   { copy 4 pixels }
                 pop     cx
                 add     di, BYTES_PER_LINE
                 sub     di, cx
                 dec     bx
                 jnz     @1

                 mov     dx, GC_INDEX
                 pop     ax
                 mov     ah, al
                 mov     al, GRAPHICS_MODE
                 out     dx, ax

           @end: pop     es
                 pop     ds
             end;*/
        }

        //procedure DrawBitmap (X, Y: Integer; var BitMap; Attr: Byte);
        //{ Bitmap starts with size W, H (Byte) }
        public static void DrawBitmap(int x, int y /*,BitMap var*/, byte attr)
        {
            int w, h;
            
            /*asm
                 push    es
                 push    ds

                 lds     si, BitMap
                 mov     ah, 0
                 cld
                 lodsb
                 mov     W, ax
                 lodsb
                 mov     H, ax
                 mov     ax, VGA_SEGMENT
                 mov     es, ax

                 mov     bl, 0
                 mov     cx, H
                 mov     dx, Y
             @1: push    cx
                 mov     cx, X
                 mov     di, W
             @2: push    cx
                 push    dx
                 or      bl, bl
                 jnz     @3
                 lodsb
                 mov     bh, al
                 mov     bl, 8
             @3: dec     bl
                 shr     bh, 1
                 jnc     @4

                 push    si
                 push    di
                 push    bx
                 mov     al, Attr

             @PutPixel:
               { CX = X, DX = Y, AL = Attr }
                 push    ax
                 mov     ax, BYTES_PER_LINE
                 mul     dx
                 push    cx
                 shr     cx, 1
                 shr     cx, 1
                 add     ax, cx
                 mov     di, ax
                 add     di, PageOffset
                 pop     cx
                 and     cl, 3
                 mov     ah, 1
                 shl     ah, cl
                 mov     al, MAP_MASK
                 mov     dx, SC_INDEX
                 out     dx, ax
                 pop     ax
                 stosb

                 pop     bx
                 pop     di
                 pop     si

             @4:
                 pop     dx
                 pop     cx
                 inc     cx
                 dec     di
                 jnz     @2

                 inc     dx
                 pop     cx
                 dec     cx
                 jnz     @1
                 pop     ds
                 pop     es
             end;
           end;*/
        }

         //??????????????????????????????????????????????????????
         /*begin
            OldScreenMode := GetMode;
         end.*/

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
