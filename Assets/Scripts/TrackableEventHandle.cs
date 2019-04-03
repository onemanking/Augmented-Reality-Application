using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackableEventHandle : MonoBehaviour, ITrackableEventHandler
{
    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    // Start is called before the first frame update
    void Start ()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour> ();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler (this);
    }

    void OnDestroy ()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler (this);
    }

    public void OnTrackableStateChanged (TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log ("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound ();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
            newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log ("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost ();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost ();
        }
    }

    protected virtual void OnTrackingFound ()
    {
        Transform childs = transform.GetComponentInChildren<Transform> (true);

        foreach (Transform child in childs)
        {
            child.gameObject.SetActive (true);
        }
    }

    protected virtual void OnTrackingLost ()
    {
        Transform childs = transform.GetComponentInChildren<Transform> (true);

        foreach (Transform child in childs)
        {
            child.gameObject.SetActive (false);
        }
    }
}