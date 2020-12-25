
void setup() {


bitClear(ADCSRA,ADPS0); 
bitSet(ADCSRA,ADPS1); 
bitClear(ADCSRA,ADPS2);

  Serial.begin(115200);
  Serial.setTimeout(200);

  pinMode(A5,INPUT);
  pinMode(A4,INPUT);
  
  }


const int o = 659; // change this number for wider sample
int data[o];
double te;
int d = 0;


void SendData(int* data,int n, double te){

  
  Serial.write(byte(9));       // begin flag
  Serial.write(byte(15));      // field value flag
  Serial.println(te);    // field value: special time
  Serial.write(byte(15));       // field value flag
  Serial.println(n);     // field value number of samples
  Serial.write(byte(15));       // field value flag

  for(int i=0;i<n;i++){
       Serial.write(highByte(data[i]));   // sending int array with special stuff
       Serial.write(lowByte(data[i]));
   }
  
  Serial.write(byte(14));    // end flag
  Serial.println();
  
  }


void readCommand(){
  String cmd =Serial.readString();
  if(cmd.charAt(0) == 'D'){
      cmd.replace("D ","");
      d = cmd.toInt();
      digitalWrite(13,LOW);
    }
  
  }


void loop() {

  if(Serial.available()>0)
    {readCommand();
    digitalWrite(13,HIGH);
    }
   te = micros();
  for(int i=0;i<o;i++){
    data[i] = analogRead(A0);
    // give delay to change absolute sample rate that will affect the te
    delayMicroseconds(d);
  }
  te = micros() - te;
  SendData(data,o,te);
  //Serial.println(te);
  
}
