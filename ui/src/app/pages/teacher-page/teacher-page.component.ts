import {Component, OnInit} from '@angular/core';
import {LogoComponent} from '../../components/logo/logo.component';
import FooterComponent from '../../components/footer/footer.component';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-teacher-page',
  imports: [
    LogoComponent,
    FooterComponent
  ],
  templateUrl: './teacher-page.component.html',
  styleUrl: './teacher-page.component.scss'
})
export class TeacherPageComponent implements OnInit {
  teacherId: string | null = null;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.teacherId = params['teacherId'];
      console.log('Teacher ID:', this.teacherId);
      // Можно сделать запрос на сервер с этим ID, например, получить информацию о преподавателе
    });
  }
}
