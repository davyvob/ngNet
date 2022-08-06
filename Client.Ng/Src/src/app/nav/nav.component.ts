import { Component, HostListener, OnInit } from '@angular/core';
import {faPlusMinus} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  
  faPlusMinus = faPlusMinus;
  isMenuOpen = true;
  public getScreenWidth: any;
  constructor() { }

  ngOnInit(): void {
  }

  toggleSideNav(){
   this.isMenuOpen = !this.isMenuOpen;
  }

  @HostListener('window:resize', ['$event'])
  onWindowResize() {

    this.getScreenWidth = window.innerWidth;
    if(this.getScreenWidth >600){
      this.isMenuOpen = true;
    }
   
    
  }

}
