- [Unity備忘録](#unity備忘録)
  - [Layer間のCollision設定](#layer間のcollision設定)
  - [DestroyとInvoke＆Coroutine](#destroyとinvokecoroutine)
  - [Width&HeightとScale](#widthheightとscale)
  - [ColliderとTrigger](#colliderとtrigger)

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