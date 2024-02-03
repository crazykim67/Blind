using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightStep : Step
{
    public override void OnStep()
    {
        StartCoroutine(ShowStep());
    }

    public override IEnumerator ShowStep()
    {
        // �ʱ�ȭ
        alpha = 0f;
        intensity = 0f;
        ren.color = new Color(ren.color.r, ren.color.g, ren.color.b, alpha);
        spotLight.intensity = intensity;

        while (alpha < 1f || intensity < 1f)
        {
            alpha += showSpeed * Time.deltaTime;
            ren.color = new Color(ren.color.r, ren.color.g, ren.color.b, alpha);

            intensity += showSpeed * Time.deltaTime;
            spotLight.intensity = intensity;

            yield return null;
        }

        yield return new WaitForSeconds(2f);

        ReturnStep();
    }

    public override void ReturnStep()
    {
        StartCoroutine(HideStep());
    }

    public override IEnumerator HideStep()
    {
        // �ʱ�ȭ
        alpha = 1f;
        intensity = 1f;
        ren.color = new Color(ren.color.r, ren.color.g, ren.color.b, alpha);
        spotLight.intensity = intensity;

        while (alpha > 0f || intensity > 0f)
        {
            alpha -= hideSpeed * Time.deltaTime;
            ren.color = new Color(ren.color.r, ren.color.g, ren.color.b, alpha);

            intensity -= hideSpeed * Time.deltaTime;
            spotLight.intensity = intensity;

            yield return null;
        }

        StepDisable();
        StepPoolManager.Instance.ReturnStep(this);
    }

    public override void StepDisable()
    {
        // �ʱ�ȭ
        alpha = 0f;
        intensity = 0f;
        ren.color = new Color(ren.color.r, ren.color.g, ren.color.b, alpha);
        spotLight.intensity = intensity;

        this.gameObject.SetActive(false);
    }
}