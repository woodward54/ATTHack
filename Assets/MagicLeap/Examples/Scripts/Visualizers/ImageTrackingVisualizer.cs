// %BANNER_BEGIN%
// ---------------------------------------------------------------------
// %COPYRIGHT_BEGIN%
//
// Copyright (c) 2019 Magic Leap, Inc. All Rights Reserved.
// Use of this file is governed by the Creator Agreement, located
// here: https://id.magicleap.com/creator-terms
//
// %COPYRIGHT_END%
// ---------------------------------------------------------------------
// %BANNER_END%

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

namespace MagicLeap
{
    /// <summary>
    /// This class handles visibility on image tracking, displaying and hiding prefabs
    /// when images are detected or lost.
    /// </summary>
    [RequireComponent(typeof(MLImageTrackerBehavior))]
    public class ImageTrackingVisualizer : MonoBehaviour
    {
        #region Private Variables
        private MLImageTrackerBehavior _trackerBehavior = null;
        private bool _targetFound = false;

        //[SerializeField, Tooltip("Text to update on ImageTracking changes.")]
        //private Text _statusLabel = null;
        // Stores initial text
        //private string _prefix;
        //private string _eventString;


        [SerializeField, Tooltip("Game Object showing the tracking cube")]
        private GameObject _trackingCube = null;

        [SerializeField]
        public GameObject _markerManager = null;


        private ImageTrackingExample.ViewMode _lastViewMode = ImageTrackingExample.ViewMode.All;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Validate inspector variables
        /// </summary>
        void Awake()
        {
            Debug.Log("Image Start");
            if (null == _trackingCube)
            {
                Debug.LogError("Error: ImageTrackingVisualizer._trackingCube is not set, disabling script.");
                enabled = false;
                return;
            }
            /*if (null == _statusLabel)
            {
                Debug.LogError("Error: ImageTrackingVisualizer._statusLabel is not set, disabling script.");
                enabled = false;
                return;
            }*/
        }

        /// <summary>
        /// Initializes variables and register callbacks
        /// </summary>
        void Start()
        {
            
            //_prefix = _statusLabel.text;
            //_statusLabel.text = _prefix + "Target Lost";
            //_eventString = "";
            _trackerBehavior = GetComponent<MLImageTrackerBehavior>();
            _trackerBehavior.OnTargetFound += OnTargetFound;
            _trackerBehavior.OnTargetLost += OnTargetLost;

            RefreshViewMode();
        }

        private void Update()
        {
            //_statusLabel.text = String.Format("{0}[{1}/{2}] {3}", _prefix, _trackerBehavior.IsTracking, _trackerBehavior.TrackingStatus, _eventString);
        }

        /// <summary>
        /// Unregister calbacks
        /// </summary>
        void OnDestroy()
        {
            _trackerBehavior.OnTargetFound -= OnTargetFound;
            _trackerBehavior.OnTargetLost -= OnTargetLost;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Update which objects should be visible
        /// </summary>
        /// <param name="viewMode">Contains the mode to view</param>
        public void UpdateViewMode(ImageTrackingExample.ViewMode viewMode)
        {
            _lastViewMode = viewMode;
            RefreshViewMode();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// De/Activate objects to be hidden/seen
        /// </summary>
        private void RefreshViewMode()
        {
            switch (_lastViewMode)
            {
                case ImageTrackingExample.ViewMode.All:
                    _trackingCube.SetActive(_targetFound);
                    break;
                case ImageTrackingExample.ViewMode.AxisOnly:
                    _trackingCube.SetActive(false);
                    break;
                case ImageTrackingExample.ViewMode.TrackingCubeOnly:
                    _trackingCube.SetActive(_targetFound);
                    break;
                case ImageTrackingExample.ViewMode.DemoOnly:
                    _trackingCube.SetActive(false);
                    break;
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Callback for when tracked image is found
        /// </summary>
        /// <param name="isReliable"> Contains if image found is reliable </param>
        private void OnTargetFound(bool isReliable)
        {
            //_eventString = String.Format("Target Found ({0})", (isReliable ? "Reliable" : "Unreliable"));
            Debug.Log("INFO: Tracker found.");
            _targetFound = true;
            _markerManager.SetActive(true);
            RefreshViewMode();
        }

        /// <summary>
        /// Callback for when image tracked is lost
        /// </summary>
        private void OnTargetLost()
        {
            //_eventString = "Target Lost";
            _targetFound = false;
            Debug.Log("ERROR: Tracking lost.");
           // RefreshViewMode();
        }
        #endregion
    }
}
