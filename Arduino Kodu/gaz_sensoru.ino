//7 6 5 4 3 2 1 0 E RW RS V0 VDD VSS 15 16
#include<LiquidCrystal.h>
LiquidCrystal lcd(3,4,5,6,7,8);
const int pinGaz=A1;
const int buzzer=13;
const int led=12;
void setup() 
{
 lcd.begin(16,2);
 pinMode(pinGaz,INPUT);
 pinMode(buzzer,OUTPUT);
 pinMode(led,OUTPUT);
 Serial.begin(9600); 
  lcd.clear();
  lcd.home();
  lcd.print("  GAZ DURUMU");
  lcd.setCursor(0,1);
  lcd.print("     NORMAL");
}

void loop() 
{
  int gazOku=analogRead(pinGaz);
//  Serial.println(gazOku);
 int okunan=Serial.read();
 if(okunan=='a')
 {
  digitalWrite(led,HIGH);
 }
 if(okunan=='b'){
  digitalWrite(led,LOW);
 }
  if(gazOku<300)
  {
    Serial.print("a");
    
    lcd.clear();
    lcd.home();
    lcd.print("  GAZ DURUMU");
    lcd.setCursor(0,1);
    lcd.print("     NORMAL");
  }
  
  else
  {
    Serial.print("b");
    lcd.clear();
    lcd.home();
    lcd.print("  GAZ DURUMU");
    lcd.setCursor(0,1);
    lcd.print("   KACAK VAR");
    digitalWrite(buzzer,HIGH);
    delay(300);
    digitalWrite(buzzer,LOW);
    delay(200);    
  }
  delay(1000);
}