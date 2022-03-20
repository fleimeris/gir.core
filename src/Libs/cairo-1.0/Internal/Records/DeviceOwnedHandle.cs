﻿namespace Cairo.Internal
{
    public partial class DeviceOwnedHandle : DeviceHandle
    {
        protected override partial bool ReleaseHandle()
        {
            Device.Destroy(this);
            return true;
        }
    }
}
