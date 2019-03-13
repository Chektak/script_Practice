업데이트해야할 것
********************************
죽었는데 또 죽는 버그
대쉬 쿨타임 다 안돌았는데 1/3 지점에서 대쉬하면 대쉬는 안돼고
대쉬쿨타임 반토막난것처럼 보이는 버그
Hp프로퍼티에 hp줄어들경우 카메라 잠시 붉어지는 효과

실험할것
********************************

Player.cs 대쉬시간을 대쉬쿨타임과 똑같이 적용

스크립트 기능추가
********************************
Meteor.cs 지면에 닿았을 시 플레이어와 가까우면 카메라 진동 효과
Player.cs Update에 있는 키보드 입력받는것들 FixedUpdate로 옮김
Player.cs bool형 canMove변수를 bool형
isDie로 변경
Player.cs Hp프로퍼티에 GameOver()로직있는곳에 isDie=true문 추가
GameManager.cs GameOver()함수에 player.canMove=false문 삭제
