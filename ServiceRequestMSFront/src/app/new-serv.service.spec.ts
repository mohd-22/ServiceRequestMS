import { TestBed } from '@angular/core/testing';

import { NewServService } from './new-serv.service';

describe('NewServService', () => {
  let service: NewServService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NewServService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
