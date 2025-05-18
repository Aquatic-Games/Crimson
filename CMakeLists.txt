cmake_minimum_required(VERSION 3.31)
project(CrimsonEngine)

set(CMAKE_CXX_STANDARD 20)

add_library(CrimsonEngine STATIC
        src/Platform/Surface.cpp
        include/CrimsonEngine/Platform/Surface.h
        include/CrimsonEngine/Core/CoreUtils.h
        include/CrimsonEngine/Math/Size.h
        src/Platform/SDL3/SDL3Surface.cpp
        src/Platform/SDL3/SDL3Surface.h)

target_include_directories(CrimsonEngine PUBLIC include)

find_package(SDL3 REQUIRED)
target_link_libraries(CrimsonEngine PUBLIC SDL3::SDL3)
