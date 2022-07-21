- [Unity備忘録](#unity備忘録)
  - [Layer間のCollision設定](#layer間のcollision設定)
  - [DestroyとInvoke＆Coroutine](#destroyとinvokecoroutine)
  - [Width&HeightとScale](#widthheightとscale)
  - [ColliderとTrigger](#colliderとtrigger)
  - [Rigidbodyとposition](#rigidbodyとposition)
  - [Unity Editorとstatic変数](#unity-editorとstatic変数)

# Unity備忘録

## Layer間のCollision設定

- 自分より下のレイヤーのみ設定可

## DestroyとInvoke＆Coroutine

- Destroy対象のオブジェクトでInvokeやCoroutineの時間差処理をしていると正常に動作しない  
  - 処理をする時間にそのコンポーネントが消えている可能性大
  - SetActive(false)で対応

## Width&HeightとScale

- Width × Heightは解像度、Scaleはサイズ  
  - サイズ調整はWidthとHeightではなく、Scaleを弄るべし

## ColliderとTrigger

- Is Triggerにチェック → Trigger化
- Colliderはぶつかる、Triggerはすり抜ける
- Rigidbodyは片方でOK

## Rigidbodyとposition

- Rigidbodyの衝突判定はUpdate前
  - transform.positionで移動すると衝突計算を全て再計算
  - rigidbody.positionで移動させるべし

## Unity Editorとstatic変数

- static変数が解放されるタイミングは次回の再生開始時(再生終了時には解放されずそのまま残る)
  - 次の再生開始時にメンバ変数を初期化
  - 新しいインスタンス生成＆古いインスタンスのデストラクタ実行
