import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientesModalComponent } from './clientes-view.component';

describe('ClientesModalComponent', () => {
  let component: ClientesModalComponent;
  let fixture: ComponentFixture<ClientesModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientesModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ClientesModalComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
