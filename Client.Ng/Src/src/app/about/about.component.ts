import { Component, OnInit } from '@angular/core';
import {faHouseCrack, faPersonRays , faChartColumn , faBomb} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {
  faHouseCrack  = faHouseCrack;
  faPersonRays = faPersonRays;
  faChartColumn = faChartColumn;
  faBomb = faBomb;
  
  constructor() { }

  ngOnInit(): void {
  }

}
