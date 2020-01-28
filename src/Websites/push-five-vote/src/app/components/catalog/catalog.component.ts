import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CatalogService } from 'src/app/services/catalog.service';
import { SongGetResult } from 'src/app/models/song-get-result';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { Song } from 'src/app/models/song';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {
  public songsResult: SongGetResult;
  private pageSize = 10;

  @Output() songChoosedEvent = new EventEmitter<Song>();

  constructor(private catalogService: CatalogService) { }

  ngOnInit() {
    this.getSongs(1, this.pageSize);
  }

  getSongs(pageIndex: number, pageSize: number): void {
    this.catalogService.getSongs(pageIndex, pageSize)
      .subscribe(songs => (this.songsResult = songs));
  }

  pageChanged(event: PageChangedEvent): void {
    this.getSongs(event.page, event.itemsPerPage);
  }
}
