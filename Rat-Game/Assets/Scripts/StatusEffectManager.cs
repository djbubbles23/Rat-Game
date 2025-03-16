using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    enum Status {
        Poison,
        Fire,
        Ice,
        NumberOfTypes
    }

    public int[] stackCount = new int[(int)Status.NumberOfTypes];
    float[] stackDurrations = new float[(int)Status.NumberOfTypes];


    void FixedUpdate()
    {
        ApplyEffects();
        DiminishStacks();
    }

    void AddStack(Status N) {
        int index = (int) N;
        stackCount[index]++;
    }

    void ApplyEffects() {
        for (int i = 0, l = stackCount.Length; i < l; i++) {
            if (stackCount[i] > 0) {
                ApplyEffect(i);
            }
        }
    }

    void ApplyEffect(int effect) {
        switch (effect) {
            case 1: //poison
            break;

            case 2: //Fire
            break;

            case 3: //Ice
            break;
        }
    }

    void DiminishStacks() {
        for (int i = 0, l = stackCount.Length; i < l; i++) {
            if (stackCount[i] > 0) {
                stackDurrations[i] += Time.deltaTime;
                if (stackDurrations[i] >= 3f) {
                    stackCount[i]--;
                    stackDurrations[i] = 0f;
                }
            }
        }
    }

}
