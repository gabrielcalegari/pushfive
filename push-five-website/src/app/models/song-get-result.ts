import { Song } from './song';

export class SongGetResult {
  public pageIndex: number;
  public pageSize: number;
  public count: number;
  public songs: Song[];
}
