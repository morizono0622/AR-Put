# AR-Put
制作期間　3日//\\
現実空間上の平面を認識し、その上をタップすると３Dオブジェクトを設置することができるスマホアプリを作成しました。
設置できるオブジェクトはSphere,Cube,Capsule,Cylinder,の４つです。
オブジェクトは複数設置可能でタッチしたオブジェクトの法線方向に沿って設置されます。
設置したオブジェクトはリストに格納され、Resetを押すことによって設置されたオブジェクトを消去できます。
右下の３つのスライダーで設置する色のRGBを設定可能でスライダー上部の四角に設置する色が表示されます。

開発過程
ARに関するアプリの制作をしようと考え、ひとまず物体を設置する方法を調べる。
平面認識した場所に物体を設置するアプリの記事[1]を見つけ、実装してみる。
一つのオブジェクトしか置けなかったのでリストを使用して複数のオブジェクトを設置できるようにする。
一種類のゲームオブジェクトのみでなく複数種類のゲームオブジェクトを設置できるようにする。
Resetボタンによってリストのオブジェクトを全て削除しリセットできるようにする。
平面にしか設置できなかったので物理レイキャストを用いてゲームオブジェクトに対してタップしたときもその場所に設置できるように設定。
平面やゲームオブジェクトに重なって設置してしまうのでオブジェクトの高さに対してオフセットを適用してなるべく重ならないようにする。
CapsuleとCylinderが選択した面に対して平行に設置されてしまうので法線方向に沿って設置されるようにする。
一色では表現の幅が狭いのでRGBを用いて色を変更できるようにする。Unity上では[0,1]の範囲でRGBを設定するが、256段階で表示した方がわかりやすいと考えたため、[0,255]でRGBを表示。
どんな色が選択されているかわかるようにスライダーの上にイメージを設置。
形状を選んぶボタンが選択されているとき暗めの色になるようにしてどれが選ばれているかわかりやすくする。

参考にしたサイト
[1]https://vracademy.jp/development/ar-foundation-introduction/
