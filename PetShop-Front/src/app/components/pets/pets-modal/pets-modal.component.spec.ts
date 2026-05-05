import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PetsModalComponent } from './pets-modal.component';

describe('PetsModalComponent', () => {
  let component: PetsModalComponent;
  let fixture: ComponentFixture<PetsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PetsModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PetsModalComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
