import { Component, OnInit } from '@angular/core';
import { Chart} from 'chart.js';
import {faArrowRotateRight , faArrowsUpToLine , faArrowRightFromBracket} from '@fortawesome/free-solid-svg-icons';
import { MasterService } from '../service/master.service';

@Component({
  selector: 'app-gamescreen',
  templateUrl: './gamescreen.component.html',
  styleUrls: ['./gamescreen.component.css']
})
export class GamescreenComponent implements OnInit {
  
  
  isInGame=false;
  isLoading=false;
  hasError=false;
  errorMessage='test';

  completionChart:any;
  rightWrongChart:any;
  levelUpChart:any;

  faRefresh = faArrowRotateRight;
  faArrowsUp = faArrowRightFromBracket;

  description:any;
  puzzleInput:any;
  
  answer:any;
  answerOutput={isCorrect:false  ,corrrectAsnwerIsHigher:false , isWithinTenDigitsOfBeingRight:false };
  higherOrLower = 'higher';
  
  constructor(private service: MasterService ) { }
  
  ngOnInit(): void {
    this.getNext();
  }

  ngAfterViewInit() {
    
    
    this.createDiagrams();
  }

  createDiagrams() {
    this.completionDiagram(20);
    this.rightWrongDiagram(70);
    this.levelUpDiagram(50);


  }

  completionDiagram(perc:number) {
  
    let color = '';
    if(perc >= 50){
      color = ' rgb(0,250,154,0.9)';
    }
    else{
      color = 'rgb(220,20,60,0.6)';
    }
    if(this.completionChart !== undefined) {
      this.completionChart.destroy();
     }
  this.completionChart = new Chart('firstDiagram' , {
    type:'doughnut',
    data : {
      labels:[],
      datasets: [{
        data:[perc , 100-perc],
        backgroundColor: [
           color,
          'rgb(255, 99, 132 , .1)'
        ],
        hoverBackgroundColor: [
          color,
          'rgb(255, 99, 132 , .3)'
        ],
        hoverBorderColor : [
          color,
          'rgb(255, 99, 132 , .3)'
        ],
      }]
    }
  })
  }


  levelUpDiagram(perc:number) {
    let color = '';
    if(perc >= 50){
      color = 'rgb(0,250,154,0.9)';
    }
    else{
      color = 'rgb(220,20,60,0.6)';
    }
    if(this.levelUpChart !== undefined) {
      this.levelUpChart.destroy();
     }
   this.levelUpChart = new Chart('thirdDiagram' , {
    type:'doughnut',
    data : {
      labels:[],
      datasets: [{
        data:[perc , 100-perc],
        backgroundColor: [
           color,
          'rgb(255, 99, 132 , .1)'
        ],
        hoverBackgroundColor: [
          color,
          'rgb(255, 99, 132 , .3)'
        ],
        hoverBorderColor : [
          color,
          'rgb(255, 99, 132 , .3)'
        ], 
              
      }]
    }
  })
  }


  rightWrongDiagram(perc:number) {
    let color = '';
    if(perc >= 50){
      color = 'rgb(0,250,154,0.9)';
    }
    else{
      color = 'rgb(220,20,60,0.6)';
    }
    if(this.rightWrongChart !== undefined) {
      this.rightWrongChart.destroy();
     }
  this.rightWrongChart = new Chart('secondDiagram' , {
    type:'doughnut',
    data : {
      labels:[],
      datasets: [{
        data:[perc , 100-perc],
        backgroundColor: [
           color,
          'rgb(255, 99, 132 , .1)'
        ],
        hoverBackgroundColor: [
          color,
          'rgb(255, 99, 132 , .3)'
        ],
        hoverBorderColor : [
          color,
          'rgb(255, 99, 132 , .3)'
        ],  
             
      }]
    }
  })
  }

  getNext(){
    this.isLoading = true;
    this.service.getnextforUser().subscribe(response => {
      console.log(response);
      this.description = response.description;
      this.puzzleInput = response.puzzleInput;
      this.isLoading = false;
      
    },
    error => {
      console.log(error);
      this.hasError = true;
      this.errorMessage = error;
      this.isLoading = false;
    }
    )
    
  }

  answerCurrent(){
   
    this.hasError = false;
    if(!this.checkIfNumber()){
      
      this.isLoading = true;  
      let answerstring = this.answer.toString();
      this.service.answerCurrentForUser(answerstring).subscribe(response => {
  
        this.answerOutput = response;
        this.answerOutput.corrrectAsnwerIsHigher ? this.higherOrLower = 'higher' : this.higherOrLower = 'lower';
        
        this.setGameState(true);
        console.log(this.isInGame)
        console.log(this.answerOutput.isCorrect)
        this.isLoading = false;
        
      },
      error => {
        console.log(error);
        this.hasError = true;
        this.errorMessage = error;
        this.isLoading = false;
      }
       )
    }
   
   
  }

  checkIfNumber(){
    Number.isNaN(Number(this.answer)) ? this.hasError = true : this.hasError = this.hasError;
    console.log(this.hasError)
    if(this.hasError) this.errorMessage = 'Please provide a numerical value';
    return this.hasError;
  }

  setGameState(inGame:any){
    this.isInGame = inGame;
    console.log(this.isInGame)
  }


}
