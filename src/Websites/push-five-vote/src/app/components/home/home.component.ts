import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { VotingComponent } from '../voting/voting.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  @ViewChild(VotingComponent, {static: true }) votingReference: VotingComponent;

  constructor() { }

  ngOnInit() {
  }
}
