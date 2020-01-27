import { Component, OnInit, Input } from '@angular/core';
import { Voting } from 'src/app/models/voting';
import { Song } from 'src/app/models/song';
import { ToastrService } from 'ngx-toastr';
import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';
import { Router } from '@angular/router';
import { VotingService } from 'src/app/services/voting.service';

@Component({
  selector: 'app-voting',
  templateUrl: './voting.component.html',
  styleUrls: ['./voting.component.scss']
})
export class VotingComponent implements OnInit {

  public voting: Voting;

  constructor(
    private toastr: ToastrService,
    private votingService: VotingService,
    private router: Router) {
    this.voting = new Voting();
  }

  vote() {
    if (this.validate()) {
      this.votingService.addVote(this.voting)
        .subscribe((voting) => {
          if (voting != null) {
            this.router.navigate(['/success']);
          }
      }, (error) => {
      });
    }
  }

  validate(): boolean {
    let isValid = true;

    if (this.voting.name == null || this.voting.name.length === 0) {
      isValid = false;
      this.toastr.error('Name is required');
    }

    if (this.voting.email == null || this.voting.email.length === 0) {
      isValid = false;
      this.toastr.error('Email is required');
    }

    if (this.voting.items.some(i => i.song == null)) {
      isValid = false;
      this.toastr.error('You should choose 5 songs');
    }

    return isValid;
  }

  setVote(song: Song) {
    const existentIndex = this.findIndex(song);
    if (existentIndex >= 0) {
      this.toastr.info('Song is already in the list');
    } else {
      const freeIndex = this.findFreeIndex();
      if (freeIndex >= 0) {
        const item = this.voting.items[freeIndex];
        item.song = song;
        this.toastr.success('Song ' + song.name + ' added to the vote list');
      } else {
        this.toastr.info('You already choosed five songs. Remove one item to change your vote.');
      }
    }
  }

  removeVote(song: Song) {
    const index = this.findIndex(song);
    if (index < 0) {
      return;
    }

    this.voting.items[index].song = null;
    this.toastr.success('Song ' + song.name + ' removed from the vote list');
  }

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.voting.items, event.previousIndex, event.currentIndex);
  }

  findFreeIndex(): number {
    for (let i = 0; i < this.voting.items.length; i++) {
      const item = this.voting.items[i];
      if (item.song == null) {
        return i;
      }
    }

    return -1;
  }

  findIndex(song: Song): number {
    return this.voting.items.findIndex(i => i.song != null && i.song.id === song.id);
  }

  ngOnInit() {
  }
}
