import { Voter } from './voter';

export class VoterGetResult {
  public pageIndex: number;
  public pageSize: number;
  public count: number;
  public voters: Voter[];
}
