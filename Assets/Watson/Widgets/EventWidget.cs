﻿/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
* @author Dogukan Erenel (derenel@us.ibm.com)
*/

using IBM.Watson.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IBM.Watson.Widgets
{

    /// <summary>
    /// This Event Widget class maps key events to a SerializedDelegate.
    /// </summary>
	public class EventWidget : Widget
    {
        #region Widget interface
        protected override string GetName()
        {
            return "Event";
        }
        #endregion

        [Serializable]
        private class Mapping
        {
            public Constants.Event m_Event = Constants.Event.NONE;
            public SerializedDelegate m_Callback = new SerializedDelegate(typeof(EventManager.OnReceiveEvent));
        };

        [SerializeField]
        private List<Mapping> m_Mappings = new List<Mapping>();

        private void OnEnable()
        {
            foreach (var mapping in m_Mappings)
            {
               EventManager.Instance.RegisterEventReceiver(mapping.m_Event, mapping.m_Callback.ResolveDelegate() as EventManager.OnReceiveEvent);
            }
        }

        private void OnDisable()
        {
            foreach (var mapping in m_Mappings)
            {
                EventManager.Instance.UnregisterEventReceiver(mapping.m_Event, mapping.m_Callback.ResolveDelegate() as EventManager.OnReceiveEvent);
            }
        }
    }

}