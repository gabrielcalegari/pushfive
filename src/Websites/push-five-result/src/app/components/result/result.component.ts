import { Component, OnInit } from '@angular/core';
import { VotingService } from 'src/app/services/voting.service';
import { VotingGetResult } from 'src/app/models/voting-get-result';
import { CatalogService } from 'src/app/services/catalog.service';
import { Song } from 'src/app/models/song';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent implements OnInit {

  public result: VotingGetResult;
  public songs: Song[];

  constructor(private votingService: VotingService, private catalogService: CatalogService) { }

  ngOnInit() {
    this.getResult();
  }

  getResult() {
    this.votingService.getResult().subscribe((result) => {

      const songIds = result.songs.map(s => s.id as Guid);
      this.catalogService.getSongsById(songIds)
          .subscribe((songsResult) => {
            this.songs = songsResult.songs;
            this.result = result;
          });
    });
  }

  getSong(id: Guid): Song {
    return this.songs.find(song => song.id === id);
  }
}
