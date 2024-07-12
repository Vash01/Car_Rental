import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCarRecordComponent } from './add-car-record.component';

describe('AddCarRecordComponent', () => {
  let component: AddCarRecordComponent;
  let fixture: ComponentFixture<AddCarRecordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddCarRecordComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddCarRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
