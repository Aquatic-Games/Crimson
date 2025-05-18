#pragma once

#include "CrimsonEngine/Core/CoreUtils.h"

namespace Crimson::Platform
{
    struct SurfaceInfo
    {
        string Name;
    };

    class Surface
    {
        static Ptr<Surface> Create(const SurfaceInfo& info);
    };
}
