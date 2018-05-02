#include "mbed.h"

DigitalOut myled(LED1);
RawSerial pc(USBTX, USBRX);
RawSerial blue(p28,p27);

void blue_recv()
{
    myled = !myled;
    if (blue.getc()=='!') {
        char msgType = blue.getc();
        if (msgType=='B') { //button data
            char button = blue.getc(); //button number/letter
            if(button == '5') pc.putc('U');
            if(button == '6') pc.putc('D');
            if(button == '7') pc.putc('L');
            if(button == '8') pc.putc('R');
            if(button == '1') pc.putc('F');
            if(button == '2') pc.putc('X');            
            blue.getc();
            blue.getc();
        }
    }
}

int main()
{
    pc.baud(57600);
    blue.attach(&blue_recv, Serial::RxIrq);

    while(1) {
        sleep();
    }
}
