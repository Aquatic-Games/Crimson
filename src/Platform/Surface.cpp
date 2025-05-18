#include "CrimsonEngine/Platform/Surface.h"

#include "SDL3/SDL3Surface.h"

namespace Crimson::Platform
{
    std::unique_ptr<Surface> Surface::Create(const SurfaceInfo& info)
    {
        return std::make_unique<SDL3::SDL3Surface>(info);
    }

}
