//test #define ANALOG_IN 0
#define FASTADC 1

#ifndef cbi
#define cbi(sfr, bit) (_SFR_BYTE(sfr) &= ~_BV(bit))
#endif
#ifndef sbi
#define sbi(sfr, bit) (_SFR_BYTE(sfr) |= _BV(bit))
#endif

void setup() {

 // set prescale to 16
 sbi(ADCSRA,ADPS2) ;
 cbi(ADCSRA,ADPS1) ;
 cbi(ADCSRA,ADPS0) ;


  Serial.begin(115200);

  pinMode(A5,INPUT);
  pinMode(A4,INPUT);
  
  }


const int o = 600; // change this number for wider sample
int data[o];
double te;



void SendData(int* data,int n, double te){

  
  Serial.write(byte(9));       // begin flag
  Serial.write(byte(15));      // field value flag
  Serial.println(te);    // field value: special time
  Serial.write(byte(15));       // field value flag
  Serial.println(n);     // field value number of samples
  Serial.write(byte(15));       // field value flag

  for(int i=0;i<n;i++){
       Serial.write(highByte(data[i]));   // sending int array with special stuff
       Serial.write( lowByte(data[i]));
   }
  
  Serial.write(byte(14));    // end flag
  Serial.println();
  
  }


void loop() {

  int d = analogRead(A5);

   te = micros();
  for(int i=0;i<o;i++)
    data[i] = analogRead(A0);
    // give delay to change absolute sample rate that will affect the te
    //delayMicroseconds(d);
  te = micros() - te;
  SendData(data,o,te);
  
}
