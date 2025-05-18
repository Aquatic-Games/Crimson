#pragma once

#include "CrimsonEngine/Core/CoreUtils.h"
#include "CrimsonEngine/Math/Size.h"

namespace Crimson::Platform
{
    struct SurfaceInfo
    {
        Math::Sizei Size;
        std::string Title;
    };

    class Surface
    {
    public:
        virtual ~Surface() = default;

        static std::unique_ptr<Surface> Create(const SurfaceInfo& info);
    };
}
