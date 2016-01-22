using System;
using System.Collections.Generic;
using KHPlayer.Classes;
using NAudio.CoreAudioApi;

namespace KHPlayer.Services
{
    public class AudioDeviceService
    {
        public IEnumerable<AudioDevice> Get()
        {
            var enumerator = new MMDeviceEnumerator();
            for (var i = 0; i < enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).Count; i++)
            {
                var endpoint = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)[i];
                yield return new AudioDevice
                {
                    Id = i,
                    Name = endpoint.FriendlyName,
                    Guid = Guid.NewGuid()
                };
            }
        }
    }
}
