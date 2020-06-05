/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { LookupTenantComponent } from './lookup-tenant.component';

describe('LookupTenantComponent', () => {
  let component: LookupTenantComponent;
  let fixture: ComponentFixture<LookupTenantComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LookupTenantComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LookupTenantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
