import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  template: `
  <footer class="footer p3">
  <!-- Copyright -->
  <div class="text-center p-3" style="background-color: rgba(0, 0, 0, 0.2)">
    © 2020 IFF-7/8 Marius Žilgužis
  </div>
  <!-- Copyright -->
</footer>
  `,
  styles: []
})
export class FooterComponent implements OnInit {
  constructor() {}
  ngOnInit() {}
}