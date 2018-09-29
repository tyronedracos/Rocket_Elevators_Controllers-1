#MAIN

import time

def main (number_of_floor, number_of_elevator):
    elevator_controller = ElevatorController(number_of_floor, number_of_elevator)
    elevator_controller.elevator_list[0].currentFloor = 3
    elevator_controller.elevator_list[0].direction = None
    elevator_controller.elevator_list[0].status = "IDLE"
    elevator_controller.elevator_list[0].floorList = []

    elevator_controller.elevator_list[1].currentFloor = 10
    elevator_controller.elevator_list[1].direction = None
    elevator_controller.elevator_list[1].status = "IDLE"
    elevator_controller.elevator_list[1].floorList = []

    #HELLO, PLEASE ENTER VALID DATA TO OPERATE THE ELEVATOR
    elevator_controller.requestElevator(1,"UP")
    elevator_controller.RequestFloor(1,6)

    elevator_controller.requestElevator(3,"UP")
    elevator_controller.RequestFloor(1,5)

    elevator_controller.requestElevator(9,"DOWN")
    elevator_controller.RequestFloor(1,2)

class Elevator:
    def __init__(self, number_of_elevator, number_of_floor):
        self.elevatorNumber = number_of_elevator
        self.currentFloor = 1
        self.direction = None
        self.status = "idle"
        self.floorList = []
        self.internalButton_list = []
        for j in range(number_of_floor): #(let j = 1; j <= number_of_floor; j++){
            self.internalButton_list.append(InternalButton(number_of_elevator, "off", j))
        
       
class Button:
    def __init__(self, direction, floor, status):
        self.floor = floor
        self.direction = direction
        self.status = status
    
 
class InternalButton:
    def __init__(self, number_of_elevator, status, floor):
        self.elevatorNumber = number_of_elevator
        self.status = status
        self.floor = floor
    

class ElevatorController:
    
    def __init__(self, number_of_floor, number_of_elevator):
        self.number_of_floor = number_of_floor
        self.number_of_elevator = number_of_elevator
        self.button_list = []
        self.elevator_list = []
        for i in range(number_of_floor):
            self.button_list.append(Button("UP",i,number_of_floor))
            self.button_list.append(Button("DOWN",i+1,number_of_floor))
        for i in range(number_of_elevator):
            self.elevator_list.append(Elevator(i,number_of_floor))
        
    #REQUEST ELEVATOR
    def requestElevator(self, floorNumber, direction):           
        Elevator = self.findElevator(floorNumber, direction, self.elevator_list)
        
        print("User is at floor ", floorNumber, "and wants to go ", direction)
        print("Elevator responding to the request: ", Elevator.elevatorNumber+1)
        

        self.bubbleSort(Elevator.floorList, Elevator.direction)
         
        self.operateElevator(Elevator, direction)
        
     
        
     
    #ACTIVATE LIGHT
   # light (floorNumber, direction, list) {    
    #    list.forEach(function(button)  {
        #        console.log(button)
          #  if ( (floorNumber == button.floor) && (direction == button.direction) ) {
           #     button.status = "on"
            #    console.log("Bouton activate:")
            #    console.log(button)
          #  };   
       # });
     #console.log (list)
    
    
    #FIND ELEVATOR
    def findElevator (self, floorNumber, direction, elevlist):
        for i in range(len(elevlist)):
            e = elevlist[i]
            
            if e.status == "STOPPED" and e.currentFloor == floorNumber and e.direction == direction:
                e.floorList.append(floorNumber)
                return e
            
            elif e.status == "IDLE" and e.currentFloor == floorNumber:
                e.floorList.append(floorNumber)   
                return e
    
            elif e.currentFloor < floorNumber and (e.status == "MOVING" or e.status == "STOPPED") and e.direction == "UP" and direction == e.direction: 
                e.floorList.append(floorNumber)
                return e
    
            elif e.currentFloor > floorNumber and (e.status == "MOVING" or e.status == "STOPPED") and e.direction == "DOWN" and direction == e.direction:
                e.floorList.append(floorNumber)
                return e

            elif e.status == "IDLE":
                e.floorList.append(floorNumber)
                return e
                
            #elif i+1 == elevlist.length:
              #  e.floorList.push(floorNumber)
              #  return e
            
             
        
    
    #shortestList(elevlist){
       # var length = 9999
       
        #for(var i = 0; i < elevlist.length; i++){
         #   if( length > elevlist[i].floorList.length){
         #       length = elevlist[i].floorList.length
         #       var r = elevlist[i]
          #  }
        #}
      #  return r;
   # }*/
   
    #BUBBLESORT
    #CRESCENT
    def bubbleSort(self, floorList, direction):
        if direction == "UP":
            for passnum in range(len(floorList)-1,0,-1):
                for i in range(passnum):
                    if floorList[i]>floorList[i+1]:
                        temp = floorList[i]
                        floorList[i] = floorList[i+1]
                        floorList[i+1] = temp
    #DESCENDING
    
        elif direction == "DOWN":
            for passnum in range(len(floorList)-1,0,-1):
                for i in range(passnum):
                    if floorList[i]<floorList[i+1]:
                        temp = floorList[i]
                        floorList[i] = floorList[i+1]
                        floorList[i+1] = temp
        print("Sorted floorList : ", floorList)

    #OPERATE ELEVATOR    
    def operateElevator(self, Elevator, direction):
        
        print("next item on floor list : ", Elevator.floorList[0])
            
        while (len(Elevator.floorList) > 0):
            
            print("current floor : ", Elevator.currentFloor)
            if (Elevator.floorList[0] == Elevator.currentFloor):
                self.openDoor(Elevator, direction)
                Elevator.floorList.pop(0)
                print("new list : ",Elevator.floorList)
                #print("next destination : ", Elevator.floorList[0])
            
            elif Elevator.floorList[0] > Elevator.currentFloor:
                self.moveUp(Elevator)
            
            elif Elevator.floorList[0] < Elevator.currentFloor:
                self.moveDown(Elevator)
            
        if (len(Elevator.floorList) < 1):
                Elevator.status = "IDLE"
                Elevator.direction = None
                print("Elevator is ", Elevator.status)
            
    
    
    #MOVE UP
    def moveUp(self, Elevator):
        if Elevator.status =="IDLE" or Elevator.status =="STOPPED":
            Elevator.status = "MOVING"
            Elevator.direction = "UP"
            print("elevator start moving", Elevator.direction)
        self.timer(2)
        Elevator.currentFloor = Elevator.currentFloor+1
        
    
    def moveDown(self, Elevator):
        if Elevator.status =="IDLE" or Elevator.status =="STOPPED":
            Elevator.status = "MOVING"
            Elevator.direction = "DOWN"
            print("elevator start moving", Elevator.direction)
        self.timer(2)
        Elevator.currentFloor = Elevator.currentFloor-1
       
    
    def openDoor(self, Elevator, direction):
        if( Elevator.status == "IDLE"):
            Elevator.direction = direction  
            Elevator.status = "STOPPED"
        
            print("Elevator is ",Elevator.status)

    #TIMER
        self.timer(2)
        print("DOOR OPENING")
        self.timer(2)
        print("DOOR IS OPEN")
        self.timer(2)
        self.closeDoor(Elevator)
    

    def closeDoor(self, Elevator):
        print("DOOR IS CLOSING")
        self.timer(2)
        print("DOOR IS CLOSE")
    

    def timer(self, milliseconds):
       time.sleep(milliseconds)
            
        
                
    def RequestFloor(self, Elevator, RequestedFloor):
        Elevator = Elevator -1
        print("User is in elevator number ", Elevator+1, " and wants to go floor ", RequestedFloor)
        Elevator =  self.interior_floorList(Elevator, RequestedFloor,self.elevator_list)
    
        self.bubbleSort(Elevator.floorList, Elevator.direction)
       
        self.operateElevator(Elevator, RequestedFloor)          
    
    def interior_floorList(self, Elevator, RequestedFloor, elevatorList):
        for i in range(len(elevatorList)):
            if Elevator == elevatorList[i].elevatorNumber:
                elevatorList[i].floorList.append(RequestedFloor)
                return elevatorList[i]
               
               

  
 
main(10,2)