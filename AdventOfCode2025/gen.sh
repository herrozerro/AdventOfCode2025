#!/bin/bash

# ==========================================
# CONFIGURATION
# ==========================================
# specific folder names based on your structure
DAYS_DIR="Days"
DATA_DIR="Data"
NAMESPACE="AdventOfCode2025"
# ==========================================

# 1. Ensure directories exist
mkdir -p "$DAYS_DIR"
mkdir -p "$DATA_DIR"

# 2. Find the next Day Number
# Scans Days folder for DayXX.cs, sorts them, picks the last one
MAX_DAY=$(ls "$DAYS_DIR"/Day*.cs 2>/dev/null | grep -oP 'Day\K\d+' | sort -n | tail -1)

if [ -z "$MAX_DAY" ]; then
    NEXT_NUM=1
else
    NEXT_NUM=$((MAX_DAY + 1))
fi

# Format with leading zero (e.g., "Day05")
DAY_NAME=$(printf "Day%02d" $NEXT_NUM)

# 3. Define File Paths
CLASS_PATH="$DAYS_DIR/$DAY_NAME.cs"
REAL_DATA_PATH="$DATA_DIR/$DAY_NAME.txt"
TEST_DATA_PATH="$DATA_DIR/$DAY_NAME.Test.txt"

# 4. Check for conflicts
if [ -f "$CLASS_PATH" ]; then
    echo "Error: $CLASS_PATH already exists!"
    exit 1
fi

# 5. Create the Data Files (Empty)
touch "$REAL_DATA_PATH"
touch "$TEST_DATA_PATH"

# 6. Generate the C# Class
cat <<EOF > "$CLASS_PATH"
using ${NAMESPACE}.Abstractions;
using ${NAMESPACE}.Utilities;

namespace ${NAMESPACE}.Days;

public class $DAY_NAME : Day
{
    // TODO: Set test values
    protected override long PartOneTestAnswer => 0;
    protected override long PartTwoTestAnswer => 0;

    protected override long SolvePart1(bool isTest = false)
    {
        var input = FileUtility.ReadLinesFromFile(Filename(isTest));
        var solution = 0;
        
        return solution;
    }

    protected override long SolvePart2(bool isTest = false)
    {
        var input = FileUtility.ReadLinesFromFile(Filename(isTest));
        var solution = 0;
        
        return solution;
    }
}
EOF

# 7. Success Output
echo "------------------------------------------------"
echo "✅ Generated Day $NEXT_NUM"
echo "------------------------------------------------"
echo "📂 Class: $CLASS_PATH"
echo "📄 Data:  $REAL_DATA_PATH"
echo "📄 Test:  $TEST_DATA_PATH"
echo "------------------------------------------------"
