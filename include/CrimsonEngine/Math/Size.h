#pragma once
#include "CrimsonEngine/Core/CoreUtils.h"

namespace Crimson::Math
{
    template<typename T>
    struct Size
    {
        T Width;
        T Height;

        explicit Size(const T wh) : Width(wh), Height(wh) { }

        Size(const T width, const T height) : Width(width), Height(height) { }

        template<typename TOther>
        [[nodiscard]] Size<TOther> As() const
        {
            return Size(static_cast<TOther>(Width), static_cast<TOther>(Height));
        }

        [[nodiscard]] std::string ToString() const
        {
            return std::format("{}x{}", Width, Height);
        }
    };

    using Sizei = Size<int32>;
    using Sizeu = Size<uint32>;
}
