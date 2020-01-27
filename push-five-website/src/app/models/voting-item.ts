import { Song } from './song';
import { Guid } from 'guid-typescript';

export class VotingItem {
  public song: Song;

  // Used only in service
  public songId: Guid;
}
