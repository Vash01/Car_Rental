import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditRentalAgreementComponent } from './edit-rental-agreement.component';

describe('EditRentalAgreementComponent', () => {
  let component: EditRentalAgreementComponent;
  let fixture: ComponentFixture<EditRentalAgreementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditRentalAgreementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditRentalAgreementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
