using System.Collections;

namespace UnityEngine.XR.Templates.MR
{
    public class FadeMaterial : MonoBehaviour
    {
        // attached game object for fading
        public GameObject Environment;

        // fade speed length
        public float fadeSpeed;
        // value of Alpha (min,max) at which the Passthrough will be set
        [SerializeField] public float minAlpha;
        [SerializeField] public float maxAlpha;


        Coroutine m_FadeCoroutine;
        Coroutine m_FadeAmbianceCoroutine;

        public void FadeSkybox(bool visible)
        {
            if (m_FadeCoroutine != null)
                StopCoroutine(m_FadeCoroutine);

            m_FadeCoroutine = StartCoroutine(Fade(visible));
        }

        //Fade Coroutine
        public IEnumerator Fade(bool visible)
        {
            Renderer rend = Environment.transform.GetComponent<Renderer>();
            float alphaValue = rend.material.GetFloat("_Alpha");

            if (visible)
            {
                //while loop to deincrement Alpha value until object is invisible
                while (rend.material.GetFloat("_Alpha") > minAlpha)
                {
                    alphaValue -= Time.deltaTime / fadeSpeed;
                    rend.material.SetFloat("_Alpha", alphaValue);
                    yield return null;
                }
                rend.material.SetFloat("_Alpha", minAlpha);
            }
            else if (!visible)
            {
                //while loop to increment object Alpha value until object is opaque
                while (rend.material.GetFloat("_Alpha") < maxAlpha)
                {
                    alphaValue += Time.deltaTime / fadeSpeed;
                    rend.material.SetFloat("_Alpha", alphaValue);
                    yield return null;
                }
                rend.material.SetFloat("_Alpha", maxAlpha);
            }
        }

        public void FadeAmbiance(bool visible)
        {
            if (m_FadeAmbianceCoroutine != null)
                StopCoroutine(m_FadeAmbianceCoroutine);

            m_FadeCoroutine = StartCoroutine(FadeAmb(!visible));
        }

        //Ambiance Coroutine
        public IEnumerator FadeAmb(bool visible)
        {
            Renderer rend = Environment.transform.GetComponent<Renderer>();
            float alphaValue = rend.material.GetFloat("_Alpha");

            if (visible)
            {
                //while loop to deincrement Alpha value until object is invisible
                while (rend.material.GetFloat("_Alpha") > minAlpha)
                {
                    alphaValue -= Time.deltaTime / fadeSpeed;
                    rend.material.SetFloat("_Alpha", alphaValue);
                    yield return null;
                }
                rend.material.SetFloat("_Alpha", minAlpha);
            }
            else if (!visible)
            {
                //while loop to increment object Alpha value until object is opaque
                while (rend.material.GetFloat("_Alpha") < maxAlpha)
                {
                    alphaValue += Time.deltaTime / fadeSpeed;
                    rend.material.SetFloat("_Alpha", alphaValue);
                    yield return null;
                }
                rend.material.SetFloat("_Alpha", maxAlpha);
            }
        }
    }
}
