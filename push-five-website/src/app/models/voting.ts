import { VotingItem } from './voting-item';

export class Voting {
  public name: string;
  public email: string;
  public items: VotingItem[];

  constructor() {
    this.items = new Array(5);
    for (let i = 0; i < 5; i++) {
      this.items[i] = new VotingItem();
    }
  }
}
