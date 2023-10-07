import { Component } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatCardModule} from '@angular/material/card';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  showFetti: boolean = false;


  fetti() {
    // Set a delay of 5 seconds (5000 milliseconds) before showing the spinner
    setTimeout(() => {
        // Show the spinner
        this.showFetti = true;

        // Set a timeout to hide the spinner after 10 seconds (10000 milliseconds)
        setTimeout(() => {
            this.showFetti = false;
        }, 30000);
    }, 0);
}


}
