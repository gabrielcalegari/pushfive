import { Component, OnInit } from '@angular/core';
import { VotingService } from 'src/app/services/voting.service';
import { VoterGetResult } from 'src/app/models/voter-get-result';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-result-voters',
  templateUrl: './result-voters.component.html',
  styleUrls: ['./result-voters.component.scss']
})
export class ResultVotersComponent implements OnInit {

  public voterGetResult: VoterGetResult;
  private pageSize = 10;

  constructor(private votingService: VotingService) { }

  ngOnInit() {
    this.getVotersResult(1, this.pageSize);
  }

  getVotersResult(pageIndex: number, pageSize: number) {
    this.votingService.getVoterResult(pageIndex, pageSize)
      .subscribe((result) => this.voterGetResult = result);
  }

  pageChanged(event: PageChangedEvent): void {
    this.getVotersResult(event.page, event.itemsPerPage);
  }
}
