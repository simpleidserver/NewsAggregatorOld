import { Injectable } from '@angular/core';
import { MatDrawerContent } from '@angular/material/sidenav';

@Injectable({
  providedIn: 'root'
})
export class DrawerContentService {
  private drawerContent: MatDrawerContent;

  public setDrawerContent(drawerContent: MatDrawerContent) {
    this.drawerContent = drawerContent;
  }

  public getDrawerContent() {
    return this.drawerContent;
  }
}
