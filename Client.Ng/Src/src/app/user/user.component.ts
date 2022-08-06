import { Component, OnInit , ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MasterService } from '../service/master.service';
import { Chart} from 'chart.js';



@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  
  

  
  ngAfterViewInit() {

    
  }
  
  constructor(private service: MasterService ,private router:Router ) { }

  ngOnInit(): void {
    
    
  }
  
  chart:any;
  chart2:any;
  
  

  logOut(){
    this.service.clearToken();
    this.router.navigate(['']);
  }
  getNext(){
    this.service.getnextforUser().subscribe(response => {
      console.log(response);
    },
    error => {
      console.log(error);
    }
    )
  }
  
  createDiagrams() {
    this.createDiagram1(90);
    this.createDiagram2(40);
    

  }

  createDiagram1(perc:number) {
  
    let color = '';
    if(perc >= 50){
      color = 'rgb(147, 250, 165 ,0.9)';
    }
    else{
      color = 'rgb(255, 99, 132 ,0.9 )';
    }
 if(this.chart !== undefined) {
  this.chart.destroy();
 }
   this.chart = new Chart('firstDiagram' , {
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
  
  console.log(this.chart)
  }
  createDiagram2(perc:number) {
  
    let color = '';
    if(perc >= 50){
      color = 'rgb(147, 250, 165 ,0.9)';
    }
    else{
      color = 'rgb(255, 99, 132 ,0.9 )';
    }
    if(this.chart2 !== undefined) {
      this.chart2.destroy();
     }
 
  this.chart2 = new Chart('secondDiagram' , {
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

  

  


}


