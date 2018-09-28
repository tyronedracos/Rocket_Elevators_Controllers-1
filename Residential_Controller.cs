using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace aspnetapp {
    public class Program {
        public static void Main (string[] args) {
            let elevator_controller = new ElevatorController(10, 2);
            elevator_controller.elevator_list[0].currentFloor = 3;
            elevator_controller.elevator_list[0].direction = null;
            elevator_controller.elevator_list[0].status = "IDLE";
            elevator_controller.elevator_list[0].floorList = new List<int> ();

            elevator_controller.elevator_list[1].currentFloor = 10;
            elevator_controller.elevator_list[1].direction = null;
            elevator_controller.elevator_list[1].status = "IDLE";
            elevator_controller.elevator_list[1].floorList = new List<int>();
            elevator_controller.requestElevator(10,"DOWN");
            elevator_controller.RequestFloor(1,9);
            //elevator_controller.requestElevator(3,"DOWN");
            //elevator_controller.RequestFloor(1,2);
                }
        public static IWebHostBuilder CreateWebHostBuilder (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseStartup<Startup> ();
    }
   
public class Elevator {
    public int elevatorNumber;
    public int currentFloor;
    public string direction;
    public string status;
    public List<int> floorList;
    public List<int> internalButton_list;
    public Elevator (int number_of_elevator, int number_of_floor) {
        this.elevatorNumber = number_of_elevator;
        this.currentFloor = 1;
        this.direction = null;
        this.status = "idle";
        this.floorList = new List<int> ();
        this.internalButton_list = new List<int> ();
        for (int i = 0; i < number_of_floor; i++) {
            Console.WriteLine (i);
        }
    }
}
public class Button {
    private int floor;
    private string direction;
    private string status;
    public Button (int floor, string direction, string status) {
        this.floor = floor;
        this.direction = direction;
        this.status = status;
    }
}
public class ElevatorController {
    private int number_of_floor;
    private int number_of_elevator;
    private List<Button> button_list;
    private List<Elevator> elevator_list;
    public ElevatorController (int number_of_floor, int number_of_elevator) {
        this.number_of_floor = number_of_floor;
        this.number_of_elevator = number_of_elevator;
        this.button_list = new List<Button> ();
        this.elevator_list = new List<Elevator> ();
        for (int i = 1; i < number_of_floor; i++) {
            this.button_list.Add (new Button (i, "UP", "off"));
            this.button_list.Add (new Button (i + 1, "DOWN'", "off"));
        }
        for (int i = 1; i <= number_of_elevator; i++) {
            this.elevator_list.Add (new Elevator (i, number_of_floor));
        }
        Console.WriteLine (this.elevator_list);
        Console.WriteLine (this.button_list);
    }
    //REQUEST ELEVATOR
    public void requestElevator (int floorNumber, string direction) {
        // this.light(floorNumber, direction, this.button_list);
        Elevator Elevator = this.findElevator (floorNumber, direction, this.elevator_list);
        /* if(Elevator.status= "IDLE"){
             Elevator.status = "STOPPED"
             Elevator.direction = direction
         }*/
        Console.WriteLine ("Elevator choose to respond to the request:");
        Console.WriteLine (Elevator);
        this.bubbleSort (Elevator.floorList, Elevator.direction);
        this.operateElevator (Elevator, direction);
    }
    /*
    //ACTIVATE LIGHT
    light (floorNumber, direction, list) {    
        list.forEach(function(button)  {
                console.log(button)
            if ( (floorNumber == button.floor) && (direction == button.direction) ) {
                button.status = "on"
                console.log("Bouton activate:")
                console.log(button)
            };   
        });
     console.log (list)
    }
    */
    //FIND ELEVATOR
    public Elevator findElevator (int floorNumber, string direction, List<Elevator> elevlist) {
        while (true) {
            for (int i = 0; i < elevlist.Count; i++) {
                Elevator e = elevlist[i];
                Console.WriteLine (elevlist);
                if (e.status == "STOPPED" && e.currentFloor == floorNumber && e.direction == direction) {
                    e.floorList.Add (floorNumber);
                    return e;
                } else if (e.status == "IDLE" && e.currentFloor == floorNumber) {
                    e.floorList.Add (floorNumber);
                    return e;
                } else if (e.currentFloor < floorNumber && (e.status == "MOVING" || e.status == "STOPPED") && e.direction == "UP" && direction == e.direction) {
                    e.floorList.Add (floorNumber);
                    return e;
                } else if (e.currentFloor > floorNumber && (e.status == "MOVING" || e.status == "STOPPED") && e.direction == "DOWN" && direction == e.direction) {
                    e.floorList.Add (floorNumber);
                    return e;
                } else if (e.status == "IDLE") {
                    e.floorList.Add (floorNumber);
                    return e;
                } else if (i + 1 == elevlist.Count){
                    e.floorList.Add (floorNumber);
                    return e;
                }
            }
        }
    }
    /* shortestList(elevlist){
         var length = 9999
        
         for(var i = 0; i < elevlist.length; i++){
             if( length > elevlist[i].floorList.length){
                 length = elevlist[i].floorList.length
                 var r = elevlist[i]
             }
         }
         return r;
     }*/
    //BUBBLESORT
    public void bubbleSort (List<int> floorList, string direction)
    {
        if (direction == "UP") {
            bool swapped;
            do {
                swapped = false;
                for (var i = 0; i < floorList.Count - 1; i++) {
                    if (floorList[i] > floorList[i + 1]) {
                        var temp = floorList[i];
                        floorList[i] = floorList[i + 1];
                        floorList[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
        }
        //DÉCROISSANT
        else if (direction == "DOWN") {
            bool swapped;
            do {
                swapped = false;
                for (var i = 0; i < floorList.Count - 1; i++) {
                    if (floorList[i] < floorList[i + 1]) {
                        var temp = floorList[i];
                        floorList[i] = floorList[i + 1];
                        floorList[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
        }
        Console.WriteLine ("Sorted floorList : ", floorList);
    }
    public void operateElevator (Elevator Elevator, string direction) {
        Console.WriteLine ("next item on floor list : ", Elevator.floorList[0]);
        while (Elevator.floorList.Count > 0) {
            Console.WriteLine ("current floor : ", Elevator.currentFloor);
            if (Elevator.floorList[0] == Elevator.currentFloor) {
                this.openDoor (Elevator, direction);
                Elevator.floorList.RemoveAt (0);
                Console.WriteLine ("new list : ", Elevator.floorList);
                Console.WriteLine ("next destination : ", Elevator.floorList[0]);
            }
            if (Elevator.floorList[0] > Elevator.currentFloor) {
                this.moveUp (Elevator);
            }
            if (Elevator.floorList[0] < Elevator.currentFloor) {
                this.moveDown (Elevator);
            }
        }
        if (Elevator.floorList.Count < 1) {
            Elevator.status = "IDLE";
            Elevator.direction = null;
            Console.WriteLine (Elevator);
        }
    }
    public void moveUp (Elevator Elevator) {
        if (Elevator.status == "IDLE" || Elevator.status == "STOPPED") {
            Elevator.status = "MOVING";
            Elevator.direction = "UP";
            Console.WriteLine (Elevator);
        }
        Elevator.currentFloor++;
    }
    public void moveDown (Elevator Elevator) {
        if (Elevator.status == "IDLE" || Elevator.status == "STOPPED") {
            Elevator.status = "MOVING";
            Elevator.direction = "DOWN";
            Console.WriteLine ("elevator start moving", Elevator);
        }
        this.timer (2000);
        Elevator.currentFloor--;
    }
    public void openDoor (Elevator Elevator, string direction) {
        if (Elevator.status == "IDLE") {
            Elevator.direction = direction;
        }
        Elevator.status = "STOPPED";
        Console.WriteLine ("Elevator stopped :", Elevator);
        //TIMER
        this.timer (2000);
        Console.WriteLine ("DOOR OPENING");
        this.timer (2000);
        Console.WriteLine ("DOOR IS OPEN");
        this.timer (2000);
        this.closeDoor (Elevator);
    }
    public void closeDoor (Elevator Elevator) {
        Console.WriteLine ("DOOR IS CLOSING");
        this.timer (2000);
        Console.WriteLine ("DOOR IS CLOSE");
    }
    public void timer (int milliseconds) {
        System.Threading.Thread.Sleep (milliseconds);
    }
    public void RequestFloor (int Elevator, int RequestedFloor) {
        Elevator elevator = this.interior_floorList (Elevator, RequestedFloor, this.elevator_list);
        //console.log("you're in :", Elevator);
        this.bubbleSort (elevator.floorList, elevator.direction);
        this.operateElevator (elevator, null);
    }
    public Elevator interior_floorList (int Elevator, int RequestedFloor, List<Elevator> elevatorList) {
        Elevator retourElevator = null;
        for (int i = 0; i < elevatorList.Count; i++) {
            if (Elevator == elevatorList[i].elevatorNumber) {
                elevatorList[i].floorList.Add (RequestedFloor);
                retourElevator = elevatorList[i];
            }
        }
        return retourElevator;
    }
}

}